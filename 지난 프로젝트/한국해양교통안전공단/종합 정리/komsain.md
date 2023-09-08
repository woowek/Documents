>로그인처리
---
1. DB 확인 -> SSO 초기화/인증 -> 세션에 komsaUserVo 형태로 데이터를 넣음
2. 인증 확인은 세션에서 확인
```java
//helper 호출
KomsaUserVO komsaUserVO = (KomsaUserVO) KomsaUserHelper.getAuthenticatedUser();
```
```java
//helper 내부
public static Object getAuthenticatedUser() {
    if (cmnComUserService.getAuthenticatedUser() == null) {
        return new KomsaUserVO();
    }
    return cmnComUserService.getAuthenticatedUser();
}
```
```java
//세션 확인
public Object getAuthenticatedUser() {
    return RequestContextHolder.getRequestAttributes().getAttribute("komsaUserVO", RequestAttributes.SCOPE_SESSION);
}
```
>화면구성
---
    * 메인
    * 메뉴 화면 공통
>선박검사 처리 로직
---
>전자결재 연동
---
>외부 솔루션
---
1. SSO
    * komsain, 그룹웨어, ERP, E감사, CBTES SSO 연동
2. ozReport
    * 선박검사 데이터 문서 조회, 또는 pdf 변환시 사용
3. rMatechart
    * 운항관리, 통계 등에 사용
4. SMS
    * LGU+ 에서 제공하는 에이전트를 사용하며 DB INSERT 시 자동 발송
>사용 오픈소스
---
    * 도로명주소
        * 사용자 생성 시 주소검색
    * itextpdf
        * 기사란 html -> pdf 변환시 사용
    * pdf.js
        * edms 에서 pdf 문서 조회 시 사용
>기타 작업내용
-------
 * 추가 확인 필요사항
   * 세금계산서 연동
   * pdf 뷰어
   * 배치 구동 방식
   * 로깅처리
   * 권한 구성 -> 이걸 설명할 필요 있을까?