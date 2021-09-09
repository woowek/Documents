함수
===
춡처

https://nykim.work/34

>함수
---
1. 함수 리터럴
```js
function add(a,b) {
  return a+b;
}
```
2. 함수 표현식
```js
var add = function(a,b) {
  return a+b;
} 
var plus = add; 
console.log( add(1,2) ); //출력값: 3
console.log( plus(2,4) ); //출력값: 6
```
* 에러 예제 (함수 표현식에서 사용된 함수 이름은 외부 코드에서 접근할 수 없다.)
```js
var add = function sum(a,b) {
  return a+b;
}; 
console.log( add(1,2) ); //출력값: 3
console.log( sum(2,4) ); //출력값: Uncaught ReferenceError: sum is not defined
```
3. 함수 호이스팅
```js
add(5,2); //출력값: 10 
function add(x,y) {
  return x*y;
}
```
* 에러 예제 (함수 표현식의 경우 호이스팅이 일어나지 않는다.)
```js
add(5,2); //출력값: uncaught type error 
var add = function (x,y) {
  return x*y;
}
```

>함수 객체
---
    함수는 다음과 같은 동작이 가능하다.
    + 리터럴에 의한 생성
    + 변수나 배열의 요소, 객체의 프로퍼티 등에 할당 가능
    + 함수의 인자로 전달 가능
    + 함수의 리턴값으로 리턴 가능
    + 동적으로 프로퍼티를 생성 및 할당 가능
* 변수나 프로퍼티의 값으로 할당
```js
/* 변수에 함수 할당 */
var foo = 100;
var bar = function(){ return 100; };
console.log( bar() ); //출력값: 100
 
/* 프로퍼티에 함수 할당 */
var obj = {};
obj.baz = function(){ return 200; }
console.log( obj.baz() ); //출력값: 200
```
* 함수 인자로 전달
```js
var foo = function(func) {
   func(); //인자로 받은 func()함수를 호출
};
 
foo(function(){
   console.log('Function can be used as the argument.');
});
```
* 리턴값으로 활용
```js
var foo = function(){
  return function(){
    console.log('This function is the return value.');
  };
};
 
foo(); //호출되지 않음
 
var bar = foo();
bar(); //출력값: 'This function is....'
```
>함수 객체의 기본 프로퍼티
* length 프로퍼티
    * 함수에서 length는 함수의 정의된 인수 개수
* prototype 프로퍼티
    * Prototype Object
    ```js
    function Person(){}
    Person.prototype.eys = 2;
    var kim = new Person();
    
    console.log(kim.eyes); //2
    ```
    * Prototype Link

    __proto__는 객체가 생성될 때 조상이었던 함수의 Prototype Object를 가리킵니다.

    kim 객체는 Person 함수로부터 생성되었으니, kim의 __proto__ 프로퍼티는 Person 함수의 Prototype Object를 가리킵니다.
    ```js
    kim.__proto__ === Person.prototype //true
    ```
>함수의 다양한 형태
* 콜백함수
```js
const sum = (num1, num2, callback) => {
	let sum = num1 + num2; //로직 "구현"
	callback(sum); //로직 "처리"
}
 
const result = val => {
	(val > 100) ? console.log("합계는 "+val+"이며 100을 넘습니다.") : console.log("합계는 "+val+"이며 100을 넘지 않습니다.");
}
 
sum(23,147,result);
//[출력] "합계는 170이며 100을 넘습니다."
```
* 즉시 실행 함수
```js
(function (name) {
	console.log("This is the immediate function -> " + name);
})('foo');
```
* 내부 함수
```js
function parent(){
   var a = 100;
   var b = 200;
 
   //child() 내부 함수 정의
   function child(){
      var b = 300;
      console.log(a); //[출력] 100
      console.log(b); //[출력] 300
   }
   child();
 
   console.log(a); //[출력] 100
   console.log(b); //[출력] 200
}
 
parent();
child(); //[출력] 에러 뙇!!!
```
* 함수를 리턴하는 함수
```js
var self = function(){
   console.log('a');
   return function(){
     console.log('b');
   }
};
 
self = self(); //a
self(); //b
```
> 함수 호출과 this
* arguments 객체
```js
//인자 개수에 상관없이 이들 각각의 값을 모두 더해 리턴하는 함수
function sum(){
   var result = 0;
 
   for (var i=0; i<arguments.length; i++){
      result += arguments[i];
   }
   return result;
} 
console.log( sum(1,2,3) ); // 6
console.log( sum(1,2,3,4,5,6,7,8,9) ); //45
```
* 호출 패턴과 this 바인딩
    * 객체의 메서드 호출 시의 this 바인딩
    ```js
    var myWebsite = {
    owner: 'nykim',
    whois: function() {
        console.log(this.owner + "'s website.");
    }
    }
    
    var yourWebsite = {
        owner: 'yeonme'
    }
    
    yourWebsite.whois = myWebsite.whois;
            
    myWebsite.whois(); //nykim's website.
    yourWebsite.whois(); //yeonme's website.
    ```
    * 함수 호출 시의 this 바인딩
    ```js
    //전역 변수 정의
    var value = 100;
    
    //myObject 객체 생성
    var myObject = {
    value: 1,
    func1: function () {
        var that = this;
        that.value += 1;
        console.log("함수1 실행되었으며 value 값은 " + this.value);
    
        func2 = function () {
        that.value += 1;
        console.log("함수2 실행되었으며 value 값은 " + that.value);
    
        func3 = function () {
            that.value += 1;
            console.log("함수3 실행되었으며 value 값은 " + that.value);
        }
        func3();
    
        }
        func2();
    }
    };
    
    myObject.func1();
    ```
    * 생성자 함수를 호출할 때 this 바인딩
    ```js	
    //Person() 생성자 함수
    var Person = function (name) {
    // 함수 코드 실행 전
    this.name = name;
    // 함수 리턴
    }
    
    //객체 생성
    var nykim = new Person("nykim");
    console.log(nykim.name);
    ```
    * 강제 인스턴스 호출
    ```js
    function B(arg) {
    if (!(this instanceof arguments.callee)) {
        return new B(arg);
    }
    this.value = arg ? arg : 0;
    }
    
    var b = new A();
    var bb = A(5);
    
    console.log(b.value);
    console.log(bb.value);
    ```
* 함수 리턴
    * 일반 함수나 메서드는 지정된 리턴값이 없다면 undefined를 리턴한다
    ```js
    var noReturn = function () {
    console.log("리턴을 명시하지 않았다...")
    };
    
    var result = noReturn();
    console.log(result); //출력: undefined
    ```
    * 생성자 함수는 지정된 리턴값이 없다면 생성된 객체를 리턴한다
    ```js
    function Person(name, age) {
    this.name = name;
    this.age = age;
    
    return {
        name: "Anonymous",
        age: 99
    }
    }
    
    var result = new Person("nana", 27);
    console.log(result); //리턴값: "Anonymous" 객체
    ```
> 프로토타입 체이닝
* 객체 리터럴 방식으로 생성된 객체의 프로로타입 체이닝
```js
var myObject = {
  name: "nayoung",
  sayName: function () {
    console.log("My name is " + this.name);
  }
}; 
console.dir(myObject.__proto__);
myObject.sayName(); //출력: My Name is nayoung
console.log(myObject.hasOwnProperty("name")); //true
console.log(myObject.hasOwnProperty("nickName")); //false
myObject.sayNickName(); //Uncaught TypeError
console.log(myObject.lalala); //undefined
```
* 생성자 함수로 생성된 객체의 프로토타입 체이닝
```js
function Person(name, age) {
  this.name = name;
  this.age = age;
} 
//생성자 함수로 객체 생성
var nana = new Person("nana", 27); 
//프로토타입 체이닝
console.log(nana.hasOwnProperty("name")); //true 
//Person.prototype 호출
console.dir(Person.prototype); //Object 
console.dir(nana.__proto__ === Person.prototype); //true
console.dir(nana.__proto__.__proto__ === Object.prototype); //true
```
* 기본 데이터 타입 확장
```js
String.prototype.myMethod = function () {
  console.log("내가 만든 메서드!");
} 
var str = "test";
str.myMethod();
console.dir(String.prototype);
```
* 프로토타입 메서드와 this 바인딩
```js
function Person(name) {
  this.name = name;
} 
//프로토타입 객체에 메서드 정의
Person.prototype.getName = function (name) {
  return this.name;
} 
//생성자 함수로 객체 생성
var nana = new Person("nana");
console.log(nana.getName()); //출력: nana 
//프로토타입 객체에 name 프로퍼티를 동적으로 추가
Person.prototype.name = "person";
console.log(Person.prototype.getName()); //출력: person
```