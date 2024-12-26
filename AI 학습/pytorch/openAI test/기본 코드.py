from openai import OpenAI

client = OpenAI(
    api_key = "api key"
)
# 사용자 프롬프트
prompt = "Hello, how are you?"

# ChatGPT 호출
chat_completion = client.chat.completions.create(
    model="gpt-3.5-turbo",
    messages=[
        {"role": "user", "content": prompt}
    ]
)

# 응답 출력
print(chat_completion.choices[0].message.content)
