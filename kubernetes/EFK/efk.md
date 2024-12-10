출처 : https://velog.io/@tmdgk4902/EFK-ELK-%EA%B0%81%EA%B0%81%EC%9D%80-%EB%AC%B4%EC%97%87%EC%9D%B4%EA%B3%A0-%EC%99%9C-%EC%8D%A8%EC%95%BC%ED%95%A0%EA%B9%8C


1. EFK?
    - Elasticsearch, Fluentd, Kibana
    - elasticsearch : 검색엔진. 인덱싱 처리
    - Fluentd : 데이터 수집
    - Kibana : UI 제공


2. 개발서버 elasticsearch UI
<img src="images/elasticMain.png">


3. 
    - Analytics-> Discover
        * 특정 namespace 로그 탐색 필요 시
            - add filter -> kubernetes.namespace 선택
            - operator -> is 선택
            - value에 namespace 선택
<img src="images/discover.png">
            - 왼쪽에 컬럼 추가
