- NFS 서버를 구성해야 할 필요성이 있어 작업 이력을 메모해놓는다.



- 서버
    * yum install -y nfs-utils 로 NFS를 설치한다.
    * 심볼릭링크로 적용시 권한문제가 있을 수 있어 mount로 처리한다.
        - 원본과 대상 폴더가 모두 존재하는 상태에서 "mount --bind 원본경로 대상경로" 의 형태로 지정한다.
        - 폴더 권한은 둘 모두 777로 적용한다.
    * vi /etc/exports 로 공유폴더를 적용한다.
        - "/nfsdir 10.0.0.0/16(rw,no_root_squash,sync)" 의 형태이다.
        - 경로는 바인드 대상 경로로 지정한다.
        ```
        exportfs -ra
        systemctl enable --now nfs-server
        ```
    - /etc/fstab 파일을 수정하여 재부팅 시에도 적용하도록 한다.
         ```
         서버IP:<원격 공유 경로>   <로컬 마운트 지점> nfs defaults 0 0
         ```
    * exportfs -ra 로 폴더 공유정보를 적용한다.
    * exportfs -v로 폴더 공유정보 확인이 가능하다.
    * 방화벽을 오픈한다.
        ```sh
        firewall-cmd --permanent --add-port=2049/tcp
        firewall-cmd --permanent --add-port=2049/udp
        firewall-cmd --permanent --add-port=111/tcp
        firewall-cmd --permanent --add-port=111/udp
        firewall-cmd --permanent --add-service=nfs
        firewall-cmd --permanent --add-service=mountd
        firewall-cmd --permanent --add-service=rpc-bind
        firewall-cmd --reload
        ```
    * nfs 서버를 실행한다.
        ```sh
        systemctl start rpcbind
        systemctl start nfs-server
        systemctl enable rpcbind
        systemctl enable nfs-server
        ```

    


- windows 클라이언트
    * 우선 windows기능 추가에 NFS 클라이언트를 추가한다.
    * 방화벽도 추가한다. TCP:2049 / UDP:2049 포트를 추가 오픈한다.
    * "cmd에서 showmount -e NFS서버IP" 로 연결 상태를 확인한다.
    * "mount -o anon,vers=3 NFS서버IP:/nfsdir Z:" 명령어로 추가하던 그냥 네트워크경로로 들어가던 그건 알아서 하자..