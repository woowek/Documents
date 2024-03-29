>설치
---
--->WSL에서는 docker 및 클러스터 작업이 안된다.. 나중에 개인적으로 해봐야겠다.


- wsl - ubuntu 환경에서 systemd 사용하도록 변경
* 출처 : https://learn.microsoft.com/ko-kr/windows/wsl/systemd
* 우분투 지우고 재실행하니까 된다....


- 일단.. WSL로 설치된 ubuntu로 kubectl 설치를 시도해본다.
0. 출처 : https://kubernetes.io/ko/docs/tasks/tools/install-kubectl-linux/
1. 리눅스에서 curl을 사용하여 kubectl 바이너리 설치
```
curl -LO "https://dl.k8s.io/release/$(curl -L -s https://dl.k8s.io/release/stable.txt)/bin/linux/amd64/kubectl"
```

2. 바이너리를 검증한다. (선택 사항)
```
curl -LO "https://dl.k8s.io/$(curl -L -s https://dl.k8s.io/release/stable.txt)/bin/linux/amd64/kubectl.sha256"
```
* kubectl 바이너리를 체크섬 파일을 통해 검증한다.
```
echo "$(cat kubectl.sha256)  kubectl" | sha256sum --check
```


2. kubectl 설치
```
sudo install -o root -g root -m 0755 kubectl /usr/local/bin/kubectl
```

3. 설치한 버전이 최신인지 확인한다.
```
kubectl version --client
```

4. 기본 패키지 관리 도구를 사용하여 설치
- ubuntu 작업 예정이므로 우분투 내용만 기입
    * apt 패키지 색인을 업데이트하고 쿠버네티스 apt 리포지터리를 사용하는 데 필요한 패키지들을 설치한다.
    ```
    sudo apt-get update
    sudo apt-get install -y apt-transport-https ca-certificates curl
    ```

    * 구글 클라우드 공개 사이닝 키를 다운로드한다.
    ```
    sudo curl -fsSLo /etc/apt/keyrings/kubernetes-archive-keyring.gpg https://packages.cloud.google.com/apt/doc/apt-key.gpg
    ```

    * 쿠버네티스 apt 리포지터리를 추가한다.
    ```
    echo "deb [signed-by=/etc/apt/keyrings/kubernetes-archive-keyring.gpg] https://apt.kubernetes.io/ kubernetes-xenial main" | sudo tee /etc/apt/sources.list.d/kubernetes.list
    ```

    * 새 리포지터리의 apt 패키지 색인을 업데이트하고 kubectl을 설치한다.
    ```
    sudo apt-get update
    sudo apt-get install -y kubectl
    ```
