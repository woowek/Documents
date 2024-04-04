


스프링부트 프로젝트를 수정해야하는 경우가 생겼다.
다만 이클립스를 설치할 수 없는 상황이라 이를 대체할 방법에 대한 문서이다.


1. java 17 설치
 - 보통 라이센스 등의 사유로 java 8을 썼는데 springboot 는 java 17 이상이다.
 - 현재 개발 PC가 java 17 이하라면  java 17이상의 구성이 필요하다.
 - 다른 자바 버전이 설치되어있다면 JAVA_HOME 등의 환경변수 변경이 필요하겠지...


2. 빌드
 - geaddle project 위치에서 다음 명령어로 빌드한다.
```
./gradlew build
```
 - clean이 필요한 경우 clean 명령어 사용
 ```
./gradlew clean
 ```

3. jpa
 - application.yml 설정 중 jpa란게 있다.
 - 그 중 ddl-auto 설정이 있는데 
  * create : 테이블 자동 생성(기존 테이블 삭제)
  * create-drop : 종료시 테이블 삭제
  * update : 새로운 컬럼 추가
  * validate : 검사만
  * none(default) : 아무것도 안함
 - 서음 생성시에만 create를 사용하고 그 다음엔 none 쓰면 될듯..
```
  jpa:
    hibernate:
      ddl-auto: none
    properties:
      hibernate:
        show_sql: true
        format_sql: true
        default_batch_fetch_size: 100
    open-in-view: true
```

4. 실행
 - 빌드하면 build 폴더가 생긴다. 프로젝트폴더/build/lib 로 이동하면 -0.0.1-SNAPSHOT.jar 파일이 있는데 이를 실행한다.
 - -Dspring.profiles.active 이건 application.yml 적용내용으로 보임
   * -Dspring.profiles.active=local : application-local.yml
   * -Dspring.profiles.active=prod : application-prod.yml
 ```
java -jar -Dspring.profiles.active=local subscheck-0.0.1-SNAPSHOT.jar
 ```

