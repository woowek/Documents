모바일 ezRendition 뷰어 설정
===
>설정
---
- tbl_tenant_config 테이블의 PROPERTY_NAME 값이 useMobileViewer 인 열의 PROPERTY_VALUE 값을 변경
- 0: 파일 다운로드, 1: SAT 뷰어 사용, 2: 쿠쿠닥스 뷰어 사용 (default: 0), 3: ezRendition

>폴더 연결
---
- Rendition Server(Windows Server)
    - 설치된 c\ezRendition 폴더 공유
- CentOS(Mobile Server)
    - samba, cifs 설치
    ```bash
    $yum install samba
    $yum install cifs-utils
    ```
    - 모바일 서버 내부에 ezRendition 폴더 생성 후 연결
    - ex) 모바일 서버 설치 경로 : /home/jmocha/ezEKP/ezFlow/webapps2/ezMobile
    ```bash
    $mkdir ezRendition
    $mount -t cifs -o user='administrator',password='kaoni123!@#' //아이피/ezRendition ezRendition
    ```