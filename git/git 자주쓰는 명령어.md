git 자주쓰는 명령어
===
처음 git repogitory 구성 시 쓴거

>초기 구성
---
- init
    - 저장소 폴더의 git 구성 초기화
    - 해당 폴더 이동 후 init 하거나
    - git init "경로" 로 그 경로 init
    ```bash
    $git init
    ```

- add
    - 파일 이동 후 add, commit로 repogitory 구성
    - add . 쓰면 변경된 모든 파일이 add
    - add "파일명" 쓰면 그 파일 add
    ```bash
    $git add .
    ```

- commit
    - add한 내용 commit
    ```bash
    $git commit
    ```

>초기 구성 후
---
- clone
    - 복제를 원하는 곳으로 가서 명령어 입력
    ```bash
    $git clone 계정@IP:경로
    ```
    ex)
    ```bash
    $git clone administrator@10.0.102.105:d:git/ezCareRVacc
    ```
- status
    - 상태 확인
    ```bash
    $git status
    ```
    ex)
    ```bash
    $ git status
    On branch master
    Your branch is up to date with 'origin/master'.

    Changes to be committed:
    (use "git restore --staged <file>..." to unstage)
            new file:   test1.txt
    ```
- log
    - 로그 확인
    ```bash
    $git log
    ```
    - 변경점 확인
    - 범위 정할 시 -p 뒤에 -범위 입력
    ```bash
    $git log -p
    ```
>brench
