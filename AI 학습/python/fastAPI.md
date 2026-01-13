출처 : https://brotherdan.tistory.com/40



- fastAPI 개념
    * FastAPI() 객체
        - 프레임워크의 핵심으로, 앱 전역 설정이나 이벤트 훅 등을 처리
        - @app.get(), @app.post() 데코레이터를 통해 라우트를 직접 정의할 수도 있지만, 규모가 커지면 라우터(routers)로 분리하는 것이 일반적
    * 라우트 데코레이터
        - @app.get("/users"), @app.post("/items") 등으로 간단히 경로와 메서드를 지정
        - 규모가 커질수록, APIRouter를 활용해 엔드포인트를 모듈별로 나누는 것이 권장됨
    * Dependency Injection
        - **Depends()**를 이용해 DB 세션, 인증 정보, 환경 설정 등을 깔끔하게 주입할 수 있음
        - 코드의 재사용성과 테스트 편의성을 높여주는 핵심 기능
    * Pydantic 스키마
        - 모델(BaseModel)을 통해 입력/출력 형식을 선언적(Declarative)으로 정의
        - 요청 바디나 쿼리 파라미터를 자동 검증하고, API 문서화에도 도움을 줌

- 디렉토리 구조
    app/
    ├── main.py                # FastAPI 애플리케이션 엔트리포인트
    ├── core/                  # 설정, 보안, 유틸성 모듈
    │   ├── config.py          # 환경 변수 로드, 전역 설정
    │   └── security.py        # 인증, JWT 로직 등
    ├── db/
    │   ├── base.py            # Base = declarative_base() 등
    │   ├── session.py         # DB 연결 엔진, 세션 생성
    │   └── migrations/        # Alembic 마이그레이션 폴더
    ├── models/                # SQLAlchemy 모델 정의
    │   ├── user.py
    │   ├── item.py
    │   └── ...
    ├── schemas/               # Pydantic 스키마
    │   ├── user.py
    │   ├── item.py
    │   └── ...
    ├── crud/                  # DB 처리 로직 (Create, Read, Update, Delete)
    │   ├── user.py
    │   ├── item.py
    │   └── ...
    ├── api/
    │   └── v1/                # 버전별 API (v1, v2 등)
    │       ├── endpoints/     # 실제 라우트(엔드포인트)들을 모아둔 디렉토리
    │       │   ├── user.py
    │       │   ├── item.py
    │       │   └── ...
    │       └── routers.py     # v1 라우터들을 모아 FastAPI에 등록하는 모듈
    ├── tests/                 # 테스트 코드
    │   ├── test_user.py
    │   ├── test_item.py
    │   └── ...
    └── celery_app.py          # Celery 초기화 (비동기 작업 필요 시)
    * main.py: app = FastAPI() 인스턴스를 생성하고, 필요한 라우터를 불러와 등록
    * core/: 공통 설정, 보안 관련 로직, 인증 헬퍼 함수 등
    * db/: DB 연결, 세션, 마이그레이션(Alembic) 관련
    * models/: SQLAlchemy ORM 모델
    * schemas/: Pydantic 데이터 검증/직렬화 모델
    * crud/: DB 액세스 로직(ORM 사용), 반복적인 CRUD 코드를 깔끔하게 캡슐화
    * api/: FastAPI 라우팅 코드, 엔드포인트들(Controller) 집합
    * tests/: pytest 기반 테스트

