1. 테스트 환경

2. k3s cluster
    - 마스터노드 설치 : curl -sfL https://get.k3s.io | sh -
    - 클러스터 토큰 : cat /var/lib/rancher/k3s/server/node-token
    - 워커노드 설치 : curl -sfL https://get.k3s.io | K3S_URL=https://<마스터 노드 IP>:6443 K3S_TOKEN=<클러스터 토큰> sh -
    - config 설정
        * 경로 : /etc/rancher/k3s/k3s.yaml
        * 이걸 복사해 ip 바꾸고 workernode에 넣는다.
    - workerNode 재설치
        * /usr/local/bin/k3s-uninstall.sh

3. k3s 설치
    - curl -sfL https://get.k3s.io | sh -
    - shell 자동완성화
        * yum install bash-completion
        * source /usr/share/bash-completion/bash_completion
        * echo 'source <(kubectl completion bash)' >>~/.bashrc
        * echo 'alias k=kubectl' >>~/.bashrc
        * echo 'complete -o default -F __start_kubectl k' >>~/.bashrc
        * exec bash
    - nginx로 테스트해보자...
        * kubectl create deploy kubecost-test --image=nginx  -n kubecost-test
        * kubectl expose deploy kubecost-test -n kubecost-test --type=NodePort --port=31000
        * vim ingress.yaml
        * kubectl apply -f ingress.yaml
        * 이럼 일단 404페이지는 뜬다....
    - 포트 오픈
        * firewall-cmd --zone=public --permanent --add-port=31000/tcp
        * firewall-cmd --reload


4. kubecost 설치
    - Kubernetes 클라이언트 (kubectl) 설치
        * 그냥 minikube로 처리
    - Helm 설치
        * tar 설치 : sudo yum install -y tar
        * git 설치 : yum install git
        * curl -fsSL -o get_helm.sh https://raw.githubusercontent.com/helm/helm/main/scripts/get-helm-3
        * chmod 700 get_helm.sh
        * ./get_helm.sh

    - kubecost 설치
        - 설치했는데 pod가 안올라온다면 삭제 후 방화벽 끄고 재설치해보자..
            * helm uninstall kubecost -n kubecost
        ```
        helm install kubecost cost-analyzer \
        --repo https://kubecost.github.io/cost-analyzer/ \
        --namespace kubecost --create-namespace \
        --set kubecostToken="d29vd2VrNzg5QGthb25pLmNvbQ==xm343yadf98"
        ```
        - 다음 에러가 발생한다면 그 아래 명영어로 환경변수를 추가로 등록해준다..
        ```
        Error: INSTALLATION FAILED: Kubernetes cluster unreachable: Get “http://localhost:8080/version": dial tcp [::1]:8080: connect: connection refused
        ```
        ```
        echo 'export KUBECONFIG=/etc/rancher/k3s/k3s.yaml' >> ~/.bashrc
        source ~/.bashrc
        ```
        - 포트포워딩 하라는데 그냥 analyzer nodeport 등록했다.
    - 업데이트
        * helm repo update && helm upgrade kubecost kubecost/cost-analyzer -n kubecost


5. 비용
    - https://www.kubecost.com/pricing
    - 차이점 요약
        * Gov Cloud(정부 클라우드라고는 하는데 한국은 대상이 아닐듯)
        * Aggregated Cluster View(클러스터 뷰 집계)
        * Saved Reports(보고서 저장을 말하는건가?)
        * Multi-Cloud Provider Support
        * Audits(감사)
        * Custom Price Sheet(사용자 정의 가격표)
        * Native SAML/OIDC
        * RBAC
        * Support(유지보수)






출처 : https://www.opencost.io/docs/
> opencost
----
1. 



2. kubecost와의 비교
    - 따로 커스텀은 가능한거같은데.. 기능이 kubecost 무료버전보다도 훨씬 적어보인다...







