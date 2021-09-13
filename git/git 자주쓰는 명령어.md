git 자주쓰는 명령어
===
처음 git repogitory 구성 시 쓴거
>초기 저장소 구성
---
- 저장소
    - git 저장소 설정
    - 저장소 폴더 생성 후 이동 후 명령어 입력
    ```bash
    $git init --bare
    ```
- 로컬 작업 영역
    - remote add
    ```bash
    $git remote add origin administrator@10.0.102.105:d:git/ezCareRVacc.git
    ```
    - add, commit, push
    ```bash
    $git add .
    $git commit -m "init"
    $git push origin master
    ```
>init, add, commit, push
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
    - ex) init 메시지로 커밋
    ```bash
    $git commit -m "init"
    ```
- push
    - commit 한 내용 저장소로 push
    ```bash
    $git push origin master
    ```
>fatch, pull
---
- fatch
    - 저장소 적용 내용 가져오기
    ```bash
    $git fatch --all
    ```
- pull
    - 저장소 내용을 가져오기
    ```bash
    $git pull origin master
    ```
>기타
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
    $git log -p -1
    ```
>오류사항 처리
---
- pull 안될때
    ```bash
    $git fetch --all
    $git reset --hard origin/master
    ```
>brench
---
- branch 생성
    ```bash
    $git branch woowek
    $git branch
    ```
- branch 체크아웃
    ```bash
    $git checkout woowek
    ```
- add, commit
    ```bash
    $git add .
    $git commit -m "branch Test"
    ```
- master 이동 후 merge
    ```bash
    $git checkout master
    $git merge woowek
    ```
- branch 삭제
    ```bash
    $git branch -d woowek
    ```





