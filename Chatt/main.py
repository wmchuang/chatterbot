from chatterbot import ChatBot
from chatterbot.trainers import ChatterBotCorpusTrainer, ListTrainer

chat = ChatBot('Ron Obvious')

# Create a new trainer for the chat
trainer = ChatterBotCorpusTrainer(chat)

# Train the chat based on the english corpus
# trainer.train("chatterbot.corpus.chinese")
# trainer.train("./answer.yml")

# trainer = ListTrainer(chat)
# trainer.train([
#     '🌹',
#     '在吗'
# ])

# Get a response to an input statement
# chat.get_response("Hello, how are you today?")


def m(s):
    response = chat.get_response(s)
    print('{} ,confidence={}'.format(response.text, response.confidence))


while True:
    i = input('>>> ').strip()
    if i != 'exit':
        m(i)
    else:
        break
