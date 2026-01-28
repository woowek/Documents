출처 : https://wikidocs.net/289908



- 아키택처
```
┌─────────────────┐
│   사용자 (You)   │
│    질문 입력      │
└────────┬────────┘
         │
         ▼
┌─────────────────────────────────────┐
│   테스트 클라이언트 (Python Script)   │
│  ┌──────────────────────────────┐  │
│  │  1. 사용자 질문 받기           │  │
│  │  2. LLM에 질문 전달           │  │
│  │  3. LLM이 도구 호출 결정       │  │
│  │  4. MCP 서버에 도구 실행 요청  │  │
│  │  5. 결과를 LLM에 다시 전달     │  │
│  │  6. 최종 답변 생성            │  │
│  └──────────────────────────────┘  │
└───────┬─────────────────┬──────────┘
        │                 │
        ▼                 ▼
┌──────────────┐   ┌──────────────┐
│ Ollama LLM   │   │  MCP 서버     │
│ (llama3.1등) │   │ (your_server) │
│              │   │               │
│ localhost:   │   │ stdio 통신    │
│ 11434        │   │               │
│              │   │ ┌──────────┐ │
│ - 자연어 이해 │   │ │ Tool 1   │ │
│ - 도구 선택   │   │ │ Tool 2   │ │
│ - 답변 생성   │   │ │ Tool 3   │ │
│              │   │ └──────────┘ │
└──────────────┘   └──────────────┘
```


- 구성 방법
    * 내가 이전에 n8n으로 구성했을때 뭔가 됐던거같아서 gpt한테 물어봤다.
    * LangFlow 
        - 드래그 앤 드롭으로 LLM + 도구 연결
        - 웹 UI에서 바로 테스트 가능
        - MCP를 직접 지원하진 않지만, Custom Tool로 래핑 가능
    * Flowise
        - n8n 스타일의 플로우 빌더
        - LLM + Custom Tools 연결 가능
        - Docker로 빠른 실행
    * Streamlit + OpenWebUI 스타일



- mcp vs fastmcp
1. MCP (공식 SDK)
    - 제공: Anthropic의 공식 Model Context Protocol SDK
    - 저수준(low-level) API
    - 완전한 제어 가능
    - 복잡한 설정 필요
    - stdio, SSE 등 다양한 전송 방식 지원
    ```py
    from mcp.server import Server
    from mcp.server.stdio import stdio_server

    app = Server("my-server")

    @app.list_tools()
    async def list_tools():
        return [Tool(...)]  # 수동으로 스키마 정의

    @app.call_tool()
    async def call_tool(name, arguments):
        # 수동으로 라우팅 처리
        if name == "add":
            return ...
    ```
2. FastMCP
    - 제공: 커뮤니티가 만든 고수준(high-level) 래퍼
    - FastAPI 스타일의 간단한 문법
    - 자동 스키마 생성 (타입 힌트에서)
    - 적은 보일러플레이트 코드
    - MCP SDK 위에 구축됨
    ```py
    from fastmcp import FastMCP

    mcp = FastMCP("my-server")

    @mcp.tool()
    def add(a: float, b: float) -> float:
        """두 수를 더합니다"""
        return a + b  # 자동으로 스키마 생성!

    mcp.run()  # 간단한 실행
    ```


- fastmcp 
    * 설치하기
    ```sh
    # uv를 사용한 설치 (권장)
    uv pip install fastmcp

    # 또는 pip를 사용한 설치
    pip install fastmcp
    ```
    * 간단한 예제
    ```sh
    from fastmcp import FastMCP

    # 1. 서버 인스턴스 생성
    mcp = FastMCP("My First FastMCP Server 🚀")

    # 2. @mcp.tool 데코레이터로 함수를 '도구'로 등록
    @mcp.tool
    def add(a: int, b: int) -> int:
        """두 숫자를 더합니다."""
        return a + b

    # 3. 서버 실행 (스크립트가 직접 실행될 때만)
    if __name__ == "__main__":
    mcp.run()
    ```

    * 실행
    ```
    fastmcp run server.py
    ```


    * 테스트
        - claude desktop
        - cursor
        - MCP Inspector
            * 개별 함수 호출용
        ```
        npx @modelcontextprotocol/inspector
        ```
        - 직접 Python 스크립트로 테스트

        - cURL + stdio 래퍼 (Node.js 서버인 경우)
            * JSON-RPC 메시지를 직접 stdin으로 전송
        - Postman/Thunder Client (HTTP transport 사용 시)
            * REST API처럼 테스트 가능


- 샘플 소스
    * mcp client에서 시작 mcp server을 시작 후 mcp client를 구동한다.
    * 샘플 소스상에선 lifespan 내부에서 서버 실행 처리를 한다.
    ```py
    @asynccontextmanager
    async def lifespan(app: FastAPI):
        """FastAPI 라이프사이클 관리"""
        # 시작 시
        try:
            print("🔄 MCP 클라이언트 연결 중...")
            await mcp_client.connect()
        except Exception as e:
            print(f"⚠️ MCP 서버 연결 실패: {e}")
            print("⚠️ MCP 도구 없이 계속 진행합니다")
        
        yield
        
        # 종료 시
        try:
            await mcp_client.close()
            print("✅ MCP 클라이언트 연결이 종료되었습니다")
        except Exception as e:
            print(f"⚠️ MCP 클라이언트 종료 중 오류: {e}")
    ```
    ```py
    async def connect(self):
        """MCP 서버에 연결 (stdio 방식)"""
        try:
            print("🔄 MCP 서버 연결 중...")
            
            self.exit_stack = AsyncExitStack()
            
            server_params = StdioServerParameters(
                command="python",
                args=[MCP_SERVER_PATH]
            )
            
            # stdio 클라이언트로 MCP 서버 시작
            read, write = await self.exit_stack.enter_async_context(
                stdio_client(server_params)
            )
            
            # 세션 생성 및 초기화
            self.session = await self.exit_stack.enter_async_context(
                ClientSession(read, write)
            )
            
            await self.session.initialize()
            
            # 사용 가능한 도구 목록 가져오기
            tools_result = await self.session.list_tools()
            self.available_tools = [
                {
                    "name": tool.name,
                    "description": tool.description,
                    "parameters": tool.inputSchema
                }
                for tool in tools_result.tools
            ]
            
            print(f"✅ MCP 서버 연결 성공! 사용 가능한 도구: {[t['name'] for t in self.available_tools]}")
            
        except Exception as e:
            print(f"❌ MCP 서버 연결 실패: {e}")
            raise
    ```
    * MCP 서버가 분리된 경우 SSE 형태로 연결 시도를 한다.
    ```py
    # MCP 서버 설정 변경
    MCP_SERVER_URL = "http://localhost:8001/sse"  # ← 경로 변경

    async def connect(self):
        """MCP 서버에 연결 (SSE 방식)"""
        from mcp.client.sse import sse_client
        
        try:
            print(f"🔄 MCP 서버 연결 중... ({MCP_SERVER_URL})")
            
            self.exit_stack = AsyncExitStack()
            
            # SSE 클라이언트로 연결 (서버는 이미 실행 중)
            read, write = await self.exit_stack.enter_async_context(
                sse_client(MCP_SERVER_URL)  # ← URL로 연결
            )        
    ```
    |구분|	현재 (내장)|	분리 (HTTP/SSE)|	분리 (stdio)|
    |---|---|---|---|
    |서버 시작|	클라이언트가 시작|	별도로 이미 실행 중|	별도로 실행|
    |통신 방식|	stdio (파이프)|	HTTP/SSE|	stdio|
    |연결 대상|	서브프로세스|	네트워크 URL|	프로세스|
    |원격 연결|	❌|	✅|	❌|
    |라이프사이클|	클라이언트가 관리|	독립적|	독립적|
    |포트|	없음|	8001 등|	없음|

    * chat
        - call_ollama 로 LLM 서버 호출
            * 텍스트 수신 -> LLM 함수 구분 -> MCP 함수 목록 매칭 -> 함수 실행
        - ollama API
        ```py
        async def call_ollama(prompt: str, model: str = OLLAMA_MODEL) -> str:
            """Ollama API 호출"""
            async with httpx.AsyncClient(timeout=120.0) as client:
                try:
                    # /api/generate 엔드포인트 사용
                    response = await client.post(
                        f"{OLLAMA_BASE_URL}/api/generate",
                        json={
                            "model": model,
                            "prompt": prompt,
                            "stream": False
                        }
                    )
                    response.raise_for_status()
                    result = response.json()
                    return result.get("response", "")
                except httpx.HTTPError as e:
                    raise HTTPException(status_code=500, detail=f"Ollama 호출 오류: {str(e)}")
        ```
        - parse_tool_calls
        ```py
        async def parse_tool_calls(llm_response: str) -> List[Dict[str, Any]]:
            """LLM 응답에서 도구 호출 파싱"""
            # 간단한 파싱 로직 (실제로는 더 정교한 파싱이 필요할 수 있음)
            tool_calls = []            
            # "use_tool: tool_name(arg1=value1, arg2=value2)" 형식을 찾음
            import re
            pattern = r'use_tool:\s*(\w+)\((.*?)\)'
            matches = re.finditer(pattern, llm_response)            
            for match in matches:
                tool_name = match.group(1)
                args_str = match.group(2)                
                # 인자 파싱
                args = {}
                if args_str.strip():
                    for arg in args_str.split(','):
                        if '=' in arg:
                            key, value = arg.split('=', 1)
                            key = key.strip()
                            value = value.strip().strip('"\'')
                            # 타입 변환 시도
                            try:
                                value = int(value)
                            except ValueError:
                                try:
                                    value = float(value)
                                except ValueError:
                                    pass
                            args[key] = value                
                tool_calls.append({"name": tool_name, "arguments": args})            
            return tool_calls
        ```


- MCP서버를 이용한 함수호출
    * 일단 로스트아크 API 호출로 테스트를 해봤다.
    * 단일 함수 호출은 잘 되는걸 확인했다..
    * 다만 그 함수 내부 함수호출의 처리가 필요한 경우 이상한 답을 내놓기 시작을 했다.
    * 이것저것 찾아봤는데 결국 LLM 처음 호출 때 전달하는 system prompt 의 정제가 필요하단걸 알았디..



- ReAct 패턴
    * 내가 필요한 내용에 대한 해답이 될고같다.
    * 대충 내용은 내가 제시한 질문에 대해 필요한 함수를 계속 재호출 하면서 처리를 한다.
    * 점점 어려워진다......
    * AI한테 재구성 요청한 결과이다.
    ```
    mcpClient/
    ├── react/                          # ✨ ReAct 엔진
    │   ├── __init__.py
    │   ├── agent.py                    # ReAct Agent 로직
    │   └── message_manager.py          # 대화 메시지 관리
    ├── prompts/
    │   ├── __init__.py                 # (업데이트)
    │   └── react_prompts.py            # ✨ ReAct 전용 프롬프트
    ├── utils/
    │   ├── __init__.py                 # ✨ 새로 생성
    │   └── tool_converter.py           # ✨ MCP → Ollama 변환
    └── client.py                       # (완전 리팩토링)

    루트/
    ├── REACT_GUIDE.md                  # ✨ 상세 가이드
    ├── QUICK_START.md                  # ✨ 빠른 시작
    ├── test_react.py                   # ✨ 테스트 스크립트
    ├── requirements.txt                # (ollama 추가)
    └── .env                            # (ReAct 설정 추가
    ```
    
    - LLM에서 MCP 함수 호출을 위한 함수처리를  AI한테 맡겼더니  ollama.client.chat  함수를 쓰는데 문제는 지원 모델이 한정적이다. 물론 OLLAMA_HOST/api/chat API도 호출이 안된다.
        * ollama.client.chat 함수를 쓰는 이유는 MCP 함수 처리 시 json 과 닽은 형태로 구조화 하려했던건데 한글처리를 위해서는 이 형태의 처리가 불가능한가보다..
    - llama3.1:8b 모델에서만 ollama.chat 함수를 쓸 수 있는데 한국어 지원을 정상적으로 지원하지 않는다.
    - 결국 OLLAMA_HOST/api/generate API를 사용해야한다.



- 함수 중첩 처리에 대한 고찰..
    * 질문을 복합패턴을 사용하도록 질문을 던져봤다.
        * 그 결과 질문 각각의 함수 하나씩만 처리를 했다. 난 루프 처리하는걸 원했는데...
        * 그래서 AI한테 물어봤다.. 많은 질문을 했지먄 결국 결론은 다음과 같다.
    * 결론
        - "내 캐릭터 원정대 모두의 장비를 보여줘" 라는 질문을 했을 때
        - 원정대 캐릭터 모두의 장비를 조회하는 함수를 만들어야지, 원정대 목록 조회와 장비 조회를 LLM이서 중첩호출하는걸 기대해서는 안된다.
        - 다만 "내 캐릭터의 원정대와 내 캐릭터의 장비 목록을 보여줘" 라는 질문을 했을 때는 LLM이 처리가 가능하다.
        - 이 작업을 위한 layer 아키텍처가 있다곤 하지만 이도 결국 MCP 함수 구성에 쓰이는거지 LLM에서 알아서 인식하도록은 할 수 없다.

- 이 함수 중첩에 대한 내용에 따라 MCP 서버의 구성도 바꿔야한다..
    * AI 문의 후 얻은 구조는 다음과 같다.
    ```
    mcpSample/
    ├── mcpServer/
    │   ├── server.py                    # MCP 서버 엔트리포인트
    │   │
    │   ├── infrastructure/              # Layer 1: API 클라이언트 (공통)
    │   │   ├── __init__.py
    │   │   └── lostark_api_client.py   # 로스트아크 API 호출만
    │   │
    │   ├── services/                    # Layer 2: 비즈니스 로직 (재사용)
    │   │   ├── __init__.py
    │   │   ├── expedition_service.py   # 원정대 관련 로직
    │   │   ├── character_service.py    # 캐릭터 관련 로직
    │   │   ├── auction_service.py      # 경매장 관련 로직
    │   │   ├── market_service.py       # 거래소 관련 로직
    │   │   └── guild_service.py        # 길드 관련 로직
    │   │
    │   ├── formatters/                  # Layer 3: 포맷터 (출력)
    │   │   ├── __init__.py
    │   │   └── markdown_formatter.py   # Markdown 변환
    │   │
    │   └── tools/                       # Layer 4: MCP 도구 (도메인별 분류)
    │       ├── __init__.py
    │       ├── expedition_tools.py     # 원정대 도구 모음 (5-10개)
    │       ├── character_tools.py      # 캐릭터 도구 모음 (5-10개)
    │       ├── auction_tools.py        # 경매장 도구 모음 (5-10개)
    │       ├── market_tools.py         # 거래소 도구 모음 (5-10개)
    │       └── guild_tools.py          # 길드 도구 모음 (5-10개)
    │
    ├── .env
    └── requirements.txt
    ```
