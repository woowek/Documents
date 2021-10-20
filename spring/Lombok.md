LOMBOK
===
출처

https://www.daleseo.com/lombok-popular-annotations/

> Lombok
---
- 함수 자동생성
> 세팅
---
- pom.xml
```xml
<!-- Lombok -->
<dependency> 
    <groupId>org.projectlombok</groupId> 
    <artifactId>lombok</artifactId> 
    <version>1.18.16</version> 
    <scope>provided</scope> 
</dependency>
```
>@Getter, @Setter
---
- xxx라는 필드에 선언하면 자동으로 getXxx()(boolean 타입인 경우, isXxx())와 setXxx() 메소드를 생성해준다.
```java
@Getter @Setter
private String name;
```
```java
user.setName("홍길동");
String userName = user.getName();
```
>생성자 자동 생성
---
- @NoArgsConstructor : 파라미터가 없는 가본 생성자 생성
- @AllArgsConstructor : 모든 필드값을 파라미터로 받는 생성자 생성
- @RequiredArgsConstructor : final이나 @NotNull 인 필드값만 파라미터로 받는 생성자 생성
```java
@NoArgsConstructor
@RequiredArgsConstructor
@AllArgsConstructor
public class User {
  private Long id;
  @NonNull
  private String username;
  @NonNull
  private String password;
  private int[] scores;
}
```
```java
User user1 = new User();
User user2 = new User("dale", "1234");
User user3 = new User(1L, "dale", "1234", null);
```
>@ToString
---
```java
@ToString(exclude = "password")
public class User {
  private Long id;
  private String username;
  private String password;
  private int[] scores;
}
```
```java
User(id=1, username=1234, scores=[80, 70, 100])
```
>@EqualsAndHashCode
---
-  callSuper 속성을 통해 equals와 hashCode 메소드 자동 생성 시 부모 클래스의 필드까지 감안할지 안 할지에 대해서 설정할 수 있습니다.
- 즉, callSuper = true로 설정하면 부모 클래스 필드 값들도 동일한지 체크하며, callSuper = false로 설정(기본값)하면 자신 클래스의 필드 값들만 고려합니다.
```java
@EqualsAndHashCode(callSuper = true)
public class User extends Domain {
  private String username;
  private String password;
}
```
```java
User user1 = new User();
user1.setId(1L);
user1.setUsername("user");
user1.setPassword("pass");

User user2 = new User();
user1.setId(2L); // 부모 클래스의 필드가 다름
user2.setUsername("user");
user2.setPassword("pass");

user1.equals(user2);
// callSuper = true 이면 false, callSuper = false 이면 true
```
>@Data
---
- @Data는 위에서 설명드린 @Getter, @Setter, @RequiredArgsConstructor, @ToString, @EqualsAndHashCode을 한꺼번에 설정해주는 매우 유용한 어노테이션입니다.
```java
@Data
public class User {
  // ...
}
```