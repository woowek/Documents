from math import hypot
import torch
import torch.nn as nn
import torch.optim as optim

torch.manual_seed(777)

input = torch.FloatTensor([[0, 0], [0, 1], [1, 0], [1, 1]])
output = torch.FloatTensor([[0], [1], [1], [0]])

model = nn.Sequential(nn.Linear(2, 4, bias=True), 
                      nn.ReLU(),    
                      nn.Linear(4, 1, bias=True), 
                      nn.Sigmoid()
                      )

criterion = nn.BCELoss()
optimizer = optim.SGD(model.parameters(), lr=1)

epoch_cnt = 0
for epoch in range(1000 + 1):
    optimizer.zero_grad()
    hypothesis = model(input)
    cost = criterion(hypothesis, output)  # aka loss
    cost.backward()
    optimizer.step()
    #if epoch % 100 == 0:
    print("#{:06d}  cost: {:10f}".format(epoch, cost.item()))

    # for name, param in model.named_parameters():
    #     if name[2:] == "weight": #param.requires_grad:
    #         print(name, "of epoch#", epoch_cnt, '\n', param.data, '\n\n')
    
    #epoch_cnt = epoch_cnt + 1

with torch.no_grad():
    hypothesis = model(input)
    predicted = (hypothesis > 0.5).float()
    accuracy = (predicted == output)
    print(hypothesis)
    print(predicted)
    print(accuracy)
