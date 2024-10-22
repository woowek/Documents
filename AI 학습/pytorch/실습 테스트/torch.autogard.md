

>TORCH.AUTOGRAD
- 출처 : https://tutorials.pytorch.kr/beginner/blitz/autograd_tutorial.html
- 사용법
```py
import torch
from torchvision.models import resnet18, ResNet18_Weights
model = resnet18(weights=ResNet18_Weights.DEFAULT)
data = torch.rand(1, 3, 64, 64)
labels = torch.rand(1, 1000)
```