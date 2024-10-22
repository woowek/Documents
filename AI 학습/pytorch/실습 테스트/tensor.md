


>Tensor
- 출저 : https://tutorials.pytorch.kr/beginner/blitz/tensor_tutorial.html
- PyTorch에서는 텐서를 사용하여 모델의 입력과 출력뿐만 아니라 모델의 매개변수를 부호화
- NumPy의 ndarray와 매우 유사
```py
import torch
import numpy as np
```
- 텐서 초기화하기
    * 데이터로부터 직접(directly) 생성하기
    ```py
    data = [[1, 2], [3, 4]]
    x_data = torch.tensor(data)
    ```
    * NumPy 배열로부터 생성하기
    ```py
    np_array = np.array(data)
    x_np = torch.from_numpy(np_array)
    ```
    * 다른 텐서로부터 생성하기:
        - 명시적으로 재정의(override)하지 않는다면, 인자로 주어진 텐서의 속성(모양(shape), 자료형(datatype))을 유지합니다.
    ```py
    x_ones = torch.ones_like(x_data) # x_data의 속성을 유지합니다.
    print(f"Ones Tensor: \n {x_ones} \n")

    x_rand = torch.rand_like(x_data, dtype=torch.float) # x_data의 속성을 덮어씁니다.
    print(f"Random Tensor: \n {x_rand} \n")
    ```
    * 무작위(random) 또는 상수(constant) 값을 사용하기:
        - shape 은 텐서의 차원(dimension)을 나타내는 튜플(tuple)로, 아래 함수들에서는 출력 텐서의 차원을 결정합니다.
    ```py
    shape = (2, 3,)
    rand_tensor = torch.rand(shape)
    ones_tensor = torch.ones(shape)
    zeros_tensor = torch.zeros(shape)

    print(f"Random Tensor: \n {rand_tensor} \n")
    print(f"Ones Tensor: \n {ones_tensor} \n")
    print(f"Zeros Tensor: \n {zeros_tensor}")
    ```

- 텐서의 속성(Attribute)
```py
tensor = torch.rand(3, 4)

print(f"Shape of tensor: {tensor.shape}")
print(f"Datatype of tensor: {tensor.dtype}")
print(f"Device tensor is stored on: {tensor.device}")
```

- 텐서 연산(Operation)
    * GPU 사용 여부 처리
    ```py
    # GPU가 존재하면 텐서를 이동합니다
    if torch.cuda.is_available():
    tensor = tensor.to('cuda')
    print(f"Device tensor is stored on: {tensor.device}")
    ```
    * NumPy식의 표준 인덱싱과 슬라이싱:
    ```py
    tensor = torch.ones(4, 4)
    tensor[:,1] = 0
    print(tensor)
    ```
    * 텐서 합치기
    ```py
    t1 = torch.cat([tensor, tensor, tensor], dim=1)
    print(t1)
    ```
    * 텐서 곱하기
    ```py
    # 요소별 곱(element-wise product)을 계산합니다
    print(f"tensor.mul(tensor) \n {tensor.mul(tensor)} \n")
    # 다른 문법:
    print(f"tensor * tensor \n {tensor * tensor}")
    ```
    ```py
    print(f"tensor.matmul(tensor.T) \n {tensor.matmul(tensor.T)} \n")
    # 다른 문법:
    print(f"tensor @ tensor.T \n {tensor @ tensor.T}")
    ```
    * 바꿔치기 연산
    ```py
    print(tensor, "\n")
    tensor.add_(5)
    print(tensor)
    ```

- NumPy 변환(Bridge)
    * 텐서를 NumPy 배열로 변환하기 : 텐서의 변경 사항이 NumPy 배열에 반영됩니다.
    ```py
    t = torch.ones(5)
    print(f"t: {t}")
    n = t.numpy()
    print(f"n: {n}")
    ```
    * NumPy 배열을 텐서로 변환하기 : NumPy 배열의 변경 사항이 텐서에 반영됩니다.
    ```py
    n = np.ones(5)
    t = torch.from_numpy(n)
    ```