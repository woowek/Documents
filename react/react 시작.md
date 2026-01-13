


- 결국 react공부를 해야할 운명에 처했다..
- 출처 : https://narup.tistory.com/183



- 선행과정
    * node.js


- 시작
    * 테스트파일 설치
    ```
    npx create-react-app test-app
    ```
    * 실행
    ```
    npm run start
    ```
    * 보니까 구조는 대충 js에서 html 태그(jsx) 또는 css를 불러오는 형태이며, 파일 import 필요 시 상단에 import를 한다.
    ```js
    import React, { Component } from 'react';
    import Test from './Test';
    ```
    * css import 시 class는 className로 처리한다.
    * 대략 기본 구조는 이런거같다.
    ```js
    import React, { Component } from 'react';
    import Test from './Test';
    class App extends Component {
        render() {
            return (
            <>
            <h1>Hello World!</h1>
            <Test />
            </>
            );
        }
    }
    export default App;
    ```
    ```js
    import React from 'react';
    import './Test.css';
    function Test() {
        const name = 'react';
        const style = {
            backgroundColor: 'black',
            color: 'aqua',
            fontSize: 24, // 기본 단위 px
            padding: '1rem' // 다른 단위 사용 시 문자열로 설정
        }
        return (
            <>
            <div style={style}>{name}</div>
            <div className="gray-box"></div>
            </>
        );
    }
    export default Test;
    ```


- 컴포넌트 생명 주기 이벤트
    * componentDidMount() : DOM 랜더링 이벤트
    * shouldComponentUpdate(nextProps, nextState) : 진행중 처리상태인듯
    * componentWillUnmount() : DOM 삭제 이벤트

- 강의 보니까 재미없다.. 그냥 직접 만들어봐야지..