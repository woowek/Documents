Node JS Express 시작
===

출처

https://rain2002kr.tistory.com/337

>설치법
---
```bash
$npm install express --save
```

>기본 시작 페이지
---
```js
const express = require('express');
const app = express();
const port = express.env.PORT || 3000
app.get('/', (req, res) => res.send('Hello World'))
app.listen(port, () => console.log(`Example app listening at http://localhost:${port}'))

```

>페이지 전환(req.params.pageId)
---
```js 
const express = require('express')
const app = express()
const port = 3000
 
//MAIN Page 
app.get('/', function (request, response) {
    var input_pageId = ['page1','page2','page3'];
    var html =       
        `<a href="/create/${input_pageId[0]}">page1</a><br>
         <a href="/create/${input_pageId[1]}">page2</a><br>
         <a href="/create/${input_pageId[2]}">page3</a><br>`
        response.send(html);
});
 
//WEB PAGE Content load by pageId
app.get('/create/:pageId', function (request, response) {
    var filteredId = request.params.pageId
    var html = 
        `
        <h1>${filteredId}<h1>
        <a href="/">home</a>
        `
    response.send(html);
});
 
app.listen(port, () => console.log(`Example app listening at http://localhost:${port}`)) 
```

>NodeJS vs Express
---
- Node js : createServer를 사용하고 그안에서 처리
- Express : express 모듈을 불러오고 app.함수를 가지고처리
+ Node.js
```js
var http = require('http');
var url = require('url');
var app = http.createServer(function(request,response){
    var _url = request.url;
    var queryData = url.parse(_url,true).query;
    var pathname = url.parse(_url,true).pathname;
    if(pathname === '/'){..코드생략 }
    }else if(pathname === '/create'){
    }else if(pathname === '/create_process'){..코드생략 }
    }else if(pathname === '/update'){..코드생략 }
    }else if(pathname === '/update_process'){..코드생략 }
     else if(pathname === '/delete_process'){..코드생략 }
     else { response.writeHead(404); response.end('not found') } //에러처리
 ```
+ Express
```js
const express = require('express')        
const app = express()
app.get('/', function (request, response) {..코드생략 }
app.get('/page/:pageId', function(request, response){..코드생략 }
app.post('/delete_process', function (request, response){..코드생략 }
app.get('/create', function(request, response){..코드생략 }
app.post('/create_process', function (request, response) {..코드생략 }
app.get('/update/:pageId', function(request, response){..코드생략 }
app.post('/update_process', function(request, response){..코드생략 } 
```
>에러처리
---
- app.use(function(req, res, next) : 일반 오류 처리시
```js
app.use(function(req, res, next) {    
    res.status(404).send('Sorry cant find that!');
});
```
- app.use(function (err, req, res, next) : 특정 에러 처리시
```js
app.use(function (err, req, res, next) {
    console.error(err.stack)
    res.status(500).send('Something broke!')
})
```
- 인덱스 라우트 에러처리
```js
router.get('/update/:pageid',function(request, response){
    var filteredId = path.parse(request, params.pageid).base;
    var filelist = requesr.filelist;
    fs.readFile('data/${filteredId}', 'utf8', function(err, description){

    });
});
```
>정적 파일 처리
---
- 정적 파일 처리는 main.js 에서 설명드린데로 public 폴더를 혹은 다른 폴더를 express.static을 이용하여 정적 폴더로 지정후, 이후에는 그안에 있는 파일은 HTML 코드를 그대로 사용하면 됩니다. 
```js
//public 폴더를 정적폴더로 지정 
app.use(express.static('public'));
app.use('/topic', topicRouter);
app.use('/', indexRouter); 
```
```js
var html = template.html(title, list,
    '<h2>${title}</h2>
    <p>${description}</p>
    <img src="">'
    );
```
>Express 라우터
---
- main.js 에서 설명드린대로 프로젝트 구조를 가지고 갈때 index.js 에서 라우터 기능을 이용하기 위해서는 아래 코드가 필요합니다. 그리고 모듈을 export 하고 main.js에서 import 합니다. 
```js
// index.js 파일 
var express = require('express');
var router = express.Router();
var template = require('../lib/template1.js');
 
//WEB PAGE Content load 
router.get('/', function (request, response) {
    var filelist = request.filelist;
    var title = 'Welcome3';
    var descriton = 'Hello, Node.js';
    var list = template.list(filelist)
    var html = template.html(title,list,
        '<h2>${title}</h2>
        <p>${descriton}</p>
        <img src="/images/coding.jpg" style="width:1000px; height:500px; display:block; margin-top:10px;">',
        '<a href="/topic/create">create</a>'
         );
    response.send(html);
    
}); 
module.exports = router 
```
- 설명
1. express.Router 불러옴
2. 이후 코드는 app. 대신 Router로
3. main.js에서 사용하기 위해 router를 export
>프로젝트 보안 처리
---
1. 설치
```bash
npm install --save helmet 
```
2. 코드 처리
```js
app.use(helmet());
```
>프로젝트 압축 처리
---
1. 설치
```bash
npm install --save compression 
```
2. 코드 처리
```js
app.use(compression());
```
>CRUD 샘플
---
- ex) 예제 구조
+ root단
    + data
        + html.txt : 텍스트 파일 모을
    + lib
        + template.js : 공용 라이브러리
    + public / image : 스태틱 공용촐더 지정 필요
        + img.png : 이미지 파일
    + routes
        + index.js : main 페이지 역할
        + router.js : 각 create, read, update, delete 역할
    + main.js : 최초 실행 포인트
```js 
// 익스프레스 
const express = require('express');
// 미들웨어 
var bodyParser = require('body-parser');
var helmet = require('helmet')
var compression = require('compression');
// 프로젝트 라우터
var topicRouter = require('./routes/router')
var indexRouter = require('./routes/index')
 
var fs = require('fs');
const port = 3000
const app = express();
 
// parse application/x-www-form-urlencoded
app.use(bodyParser.urlencoded({ extended: false })) //바디파서를 위한 미들웨어
app.use(compression()); //압축을 위한 솔루션
app.use(helmet()); //보안을 위한 솔루션 
 
//현 프로젝트 미들웨어 만들어 사용하기(폴더 이름값을 읽는 함수를 미들웨어로 생성해서 사용)
app.get('*',function(req,res, next){
    fs.readdir('./data', function(err, filelist){
        req.filelist = filelist;
        next();
    });
});
//public 폴더를 정적폴더로 지정 
app.use(express.static('public'));
app.use('/topic', topicRouter);
app.use('/', indexRouter);
 
 
//ERROR Handlers default
app.use(function(req, res, next) {    
    res.status(404).send('Sorry cant find that!');
});
 
//SERVER ERROR(서보 오류발생)
app.use(function (err, req, res, next) {
    console.error(err.stack)
    res.status(500).send('Something broke!')
})
 
app.listen(port, () => console.log(`Example app listening at http://localhost:${port}`))  
```
- 내용
    1. 익스프레스 밑 프로젝트에 필요한 미들웨어를 가지고 옵니다. ( 이정도는 기본설치 필요 )
        - body parser: body를 분석하기 위한 미들웨어, 노드 js 에서는 request.on('data' ,function( ) 콜백으로 오는 body 값을 더하다가 ('end' 처리 정도 해줬는데 통신이 끊겼을때라든지 그런 경우에 대한 대책도 보안 이 되는것 같습니다. 지금은 코드의 량이 줄었다? 정도로 bodyParser() 미들웨어를 이해 하고 있습니다.
        - helmet : 보안을 위한 미들웨어
        - compression : 프로젝트 압축을 위한 미들웨어 
    2. 프로젝트 라우터 설정
       - 홈 역활을 하는 index 라우터와 각 CRUD 기능을 담당하고 있는 라우터를 나눴습니다. 이후 다양한 라우터를 설계하게 된다면 적절한 이름과 배치를 하면 됩니다.
    3. 노드 익스프레스 서버 세팅
        - 포트 번호와 익스프레스를 돌릴 이름을 세팅
        ```js
        const port = 3000
        const app = express();
        ```
    4. 미들웨어 만들어 사용하기
        - 프로젝트에서 공통으로 사용되어질 미들웨어를 만듭니다. 아래 사용된 *은 전체 적용입니다.
        ```js
        app.get('*',function(req,res, next){
            fs.readdir('./data', function(err, filelist){
                req.filelist = filelist;
                next();
            });
        });
        ```
        - 위 코드는 fs.readdir 로 비동기 함수 입니다. ./data 에 있는 파일목록을 읽어서 콜백으로 filelist를 받아와서, 다시 req.filelist 에 돌려줍니다. 이제 프로젝트 어디에서도 req.filelist 를 이용하면 폴더의 파일목록을 읽어올 수 있습니다.
    5. public 폴더를 정적폴더로 지정해서 사용하기
        - 이미지 파일, css파일 및 자바스크립트 파일 같은 정적파일을 제공시 익스프레스에서 static 로 폴더를 지정해야합니다.
        ```js
        app.use(bodyParser.urlencoded({ extended: false })) //바디파서를 위한 미들웨어
        app.use(compression()); //압축을 위한 솔루션
        app.use(helmet()); //보안을 위한 솔루션 
        ```
    6. 서버의 에러처리
        에러처리의 규칙
        - app.use(function(req, res, next) : 일반 오류 처리시
        ```js
        app.use(function(req, res, next) {    
            res.status(404).send('Sorry cant find that!');
        });
        ```
        - app.use(function (err, req, res, next) : 특정 에러 처리시
        ```js
        app.use(function (err, req, res, next) {
            console.error(err.stack)
            res.status(500).send('Something broke!')
        })
        ```
    7. 서버 listen
        ```js
        app.listen(port, () => console.log(`Example app listening at http://localhost:${port}`))  
        ```

>

