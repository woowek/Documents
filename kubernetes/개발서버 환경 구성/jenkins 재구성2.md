
1. 개요
    - 이번엔 springboot다.
    - 이번엔 또 맨땅에서 해야한다...
    - 이번엔 개발 소스와 배포 소스가 같이 있다.

2. 예상 과정
    - git pull -> graddle build -> docker build -> nexus push
    - 컨테이너는 대충 이전꺼 복붙해오자...

3. graddle build 는 그냥 빌드할 때 쓴 스크립트를 썼다. 그냥 되긴 하더라.


4. docker build 시점은 문제가 있긴 했다. java 17 설치때 문제가 있었는데
    - rpm 다운로드 경로 변경
        * https://download.oracle.com/java/17/archive/jdk-17_linux-x64_bin.rpm
    - 키 적용
    ```
    RUN cd /root && wget https://yum.oracle.com/RPM-GPG-KEY-oracle-ol7
    RUN cd /root && rpm --import RPM-GPG-KEY-oracle-ol7
    ```
    - findutils 설치
        * RUN yum -y install findutils

