JSTL
===
>개요
---
- 일반적으로 알고있는 JSTL이란 JSTL + EL의 조합을 말한다.
- HTML 코드 내에 java 코드인 스크립틀릿 <%= student %>를 ${student}로, <%=if %>문을 <c:if>, <%=for%>문을 <c:forEach>로 대체하여 사용한다.
>사용법
---
- JSTL은 라이브러리이기 때문에 사용하기전에 core를 header에 추가해주어야 한다.
```js
<% @taglib uri="http://java.sun.com/jstl/core" prefix="c" %>
```
- 사용방법은 다음과 같다.
```js
<c:if test=""></c:if>
<c:forEach items=""></c:forEach>
```
>태그 목록
---
|태그|기능|
|---|---|
|<c:set>|변수명에 값을 할당|
|<c:out>|값을 출력|
|<c:if>|조건식에 해당하는 블럭과 사용될 scope설정|
|<c:choose>|다른 언어의 switch와 비슷|
|<c:when>|switch문의 case에 해당|
|<c:otherwise>|switch문의 default에 해당|
|<c:forEach>|다른언어의 loop문 items 속성에 배열을 할당할 수 있음|

EL
===
>사용법
---
```js
<%= name %>
${name}
```
>EL의 내장 객체
---
|태그|기능|
|---|---|
|${pageScope}|page Scope에 접근하기 위한 객체|
|${reqeustScope}|request Scope에 접근하기 위한 객체|
|${sessionScope}|session Scope에 접근하기 위한 객체|
|${applicationScope}|application Scope에 접근하기 위한 객체|
|${param}|파라미터 값을 가져오기 위한 객체|
|${header}|헤더 값을 가져오기 위한 객체|
|${cookie}|쿠키 값을 가져오기 위한 객체|
|${initParam}|JSP 초기 파라미터를 가져오기 위한 객체|
|${pageContext}|pageContext 객체에 접근하기 위한 객체|

>스크립틀릿 vs JSTL
---
- 스크립틀릿
```js
<html>
 <body>
  <%
    for (int i = 1; i <= 10; i ++) {
  %>
      <%= i %><br/>
  <%
  }
  %>
  <%= request.getAttribute("person").getAddress().getCity() %>
 </body>
</html>
```
- JSTL
```js
<% @taglib uri="http://java.sun.com/jstl/core" prefix="c" %>
<html>
 <body>
  <c:forEach var="i" begin="1" end="10" step="1">
   <c:out value="${i}"/>
   <br/>
  </c:forEach>
  ${person.address.city}
 </body>
</html>
```
>표현 목록
---
- javascript에서 model parameter를 가져오는 방법
```js
var key = '<c:out value='${key}' />';
```
- javascript에서 context path를 가져오는 방법
```js
var G_CONTEXT_PATH = "${pageContext.request.contextPath}";
```
- jsp에서 url의 get parameter 가져오는 방법
```js
<c:if test="${param.loginFail eq 'true'}">
  <div class="login-fail">아이디 혹은 비밀번호가 일치하지 않습니다.</div>
</c:if>
```
- jsp에서 현재 년도 구하기
```js
<%@taglib prefix="fmt" uri="http://java.sun.com/jsp/jstl/fmt" %>
<jsp:useBean id="now" class="java.util.Date" />
<fmt:formatDate var="year" value="${now}" pattern="yyyy" />
<p>Current year: ${year}</p>
<p>Previous year: ${year - 1}</p>
```