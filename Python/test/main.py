import sys
import requests

def print_original_list_size():
    total = 0
    for poke in original_list:
        total = total + poke.__sizeof__()
    print('pokemon_list size: ', total, ' Bytes')
def print_refined_list_size():
    total = 0
    for poke in refined_list:
        total = total + poke.__sizeof__()
    print('refined_list size: ', total, 'Bytes')


original_list = []
refined_list = []
stop_num = 10

i = 1
t = {}

while i < stop_num:

    http_response = requests.get('https://pokeapi.co/api/v2/pokemon/{0}'.format(i))
    http_response.close()
    poke_obj = http_response.json()

    t.update({'name': poke_obj['name']})
    t.update({'base_experience': poke_obj['base_experience']})
    t.update({'index': poke_obj['id']})
    t.update({'hp': poke_obj['stats'][0]['base_stat']})


    original_list.append(poke_obj)
    refined_list.append(t.copy())
    i = i + 1

print_refined_list_size()
print_pokemon_list_size()




