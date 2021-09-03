Node.JS 시작
===
>설치
---
1. 다운로드
    - 공식사이트인 https://nodejs.org 를 통해서 설치파일을 다운로드 받을 수 있습니다.
2. 설치확인
```console
C:\Users\NC498>node -v
v14.17.6
```

>서버구축
---
1. 프로젝트 폴더 생성 후 server.js 파일 생성
```js
// 1. 서버 사용을 위해서 http 모듈을 http 변수에 담는다. (모듈과 변수의 이름은 달라도 된다.) 
var http = require('http'); 

// 2. http 모듈로 서버를 생성한다.
//    아래와 같이 작성하면 서버를 생성한 후, 사용자로 부터 http 요청이 들어오면 function 블럭내부의 코드를 실행해서 응답한다.
var server = http.createServer(function(request,response){ 
    response.writeHead(200,{'Content-Type':'text/html; charset=utf-8'});
    response.end('Hello node.js!!');
});

// 3. listen 함수로 8080 포트를 가진 서버를 실행한다. 서버가 실행된 것을 콘솔창에서 확인하기 위해 'Server is running...' 로그를 출력한다
server.listen(8080, function(){ 
    console.log('Server is running...');
});
```
2. server.js 실행 및 접속
- cmd 실행 후 프로젝트 폴더로 들어가 명령어 실행
- 종료 : ctrl + c
```console
E:\NodeJsDev\Test01>node server
server on!
```
>클라이언트 요청
---
1. 예제 내용
- url 모듈 가져오기
```js
var url = require('url');
```
- request 분해
    - url.parse : 주소값을 객체화
```js
var _url = request.url;
var queryData = url.parse(_url,true).query;
```
- 브라우저 요청값 반환
```js
response.writeHead(200, {'Content-Type':'text/html; charset=utf-8'});
response.end('var1의 값은 '+parsedQuery.var1);
```

2. 예제
```js
var http = require('http');
// 1. 요청한 url을 객체로 만들기 위해 url 모듈사용
var url = require('url');
var server = http.createServer(function(request, response){
    //GET(querystring) 예제
    var _url = request.url;
    var queryData = url.parse(_url,true).query;
    response.writeHead(200, {'Content-Type':'text/html; charset=utf-8'});
    response.end('Hello node.js!!');
    //////////////////////
});

server.listen(8080, function(){
    console.log('Server is running...');
});
```
















