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
```js
//Node js 는 createServer를 사용하고 그안에서 처리 
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
```js
//Express 는 express 모듈을 불러오고 app.함수를 가지고처리 
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


