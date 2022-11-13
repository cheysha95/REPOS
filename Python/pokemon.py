import sys
from requests import get
import random
from math import floor
import time
import os

class pokemon:
    def __init__(self,**kwargs):
        self.name = kwargs.get('name')
        self.type = kwargs.get('type')
        self.index = kwargs.get('index')
        self.base_hp = kwargs.get('base_hp')
        self.base_attack = kwargs.get('base_attk')
        self.base_defense = kwargs.get('base_def')
        self.spcl_attk = kwargs.get('spcl_attk')
        self.spcl_def = kwargs.get('spcl_def')
        self.base_speed = kwargs.get('base_speed')
        self.weight = kwargs.get('weight')
        self.level = kwargs.get('level')
        self.hp = floor(.01 * (2 * self.base_hp + 1 + floor(.25)) * self.level) + self.level + 10
        self.defense = floor(.01*(2*self.base_defense+31+floor(.25))*self.level)+5
        self.attack = floor(.01*(2*self.base_attack+31+floor(.25))*self.level)+5
        self.special_defense = floor(.01 * (2 * self.spcl_def + 31 + floor(.25)) * self.level) + 5
        self.special_attack = floor(.01 * (2 * self.spcl_attk + 31 + floor(.25)) * self.level) + 5
        self.speed = floor(.01*(2*self.base_speed+31+floor(.25))*self.level)+5
        self.moveset = kwargs.get('moves')
        self.moveset1 = kwargs.get('moveset')
    def print_stats(self):
        print('/ {0} / {1} / #{2}'.format(str.capitalize(self.name), str.capitalize(self.type),self.index))
        print('HP: {0} Atk: {1} Def: {2} Speed: {3} Weight: {4}'.format(self.hp,self.attack,self.defense,self.speed,self.weight))
        print(self.moveset)
        print('-------------------------------------')

def clear_console():
    os.system('cls')
def intro_splash():
    print('Your Bag:')
    for x in user_bag:
        x.print_stats()
    print('--------------------------------------------')
    print('Rival Bag:')
    for x in rival_bag:
        x.print_stats()
    print('=============================================================================================')
def bag_generator(*args):
    def create_poke():
        def pokemon_data_import(ran_num):
            pokemon_data_object = get('https://pokeapi.co/api/v2/pokemon/{0}'.format(ran_num))
            pokemon_data_object.close()
            pokemon_data_object = pokemon_data_object.json()
            return pokemon_data_object
        def move_gen():
            move_count = len(poke['moves']) # total number of moves pokemon can learn
            move_names = [poke['moves'][random.randrange(0, move_count)]['move']['name'],  # random move between 0 and move_count
                          poke['moves'][random.randrange(0, move_count)]['move']['name'],
                          poke['moves'][random.randrange(0, move_count)]['move']['name'],
                          poke['moves'][random.randrange(0, move_count)]['move']['name']]
            damage_list = []
            move_list1 = []
            for move in move_names:
                    move_info = get('https://pokeapi.co/api/v2/move/{0}'.format(move)) # grabs the damage value for the move in question
                    move_info.close()
                    move_info = move_info.json()
                    #rewrite
                    move_list1.append(move_info)
                    damage_list.append(move_info['power'])

            move_list = list(zip(move_names, damage_list))
            return move_list,move_list1 # move_list1 is unused but maybe in future

        ran_num = random.randrange(0, 250) # select random poke by random num, raise index value for later generations
        poke = pokemon_data_import(ran_num)
        ml = move_gen()
        move_list = ml[0] # currently used
        move_list1 = ml[1] # move_list1 unused but more detailed
        level = 100

        # stat_pass(name,type,index,hp,attack,defense)
        stat_pass = {'name': poke['name'],
                      'type': poke['types'][0]['type']['name'],
                      'index':poke['id'],
                      'base_hp':poke['stats'][0]['base_stat'],
                      'base_attk':poke['stats'][1]['base_stat'],
                      'base_def': poke['stats'][2]['base_stat'],
                      'base_speed':poke['stats'][5]['base_stat'],
                      'weight': poke['weight'],
                      'spcl_attk': poke['stats'][3]['base_stat'],
                      'spcl_def': poke['stats'][4]['base_stat']
        }
        poke = pokemon(name=stat_pass['name'],
                       type=stat_pass['type'],
                       index=stat_pass['index'],
                       base_hp=stat_pass['base_hp'],
                       base_attk=stat_pass['base_attk'],
                       base_def=stat_pass['base_def'],
                       spcl_attk=stat_pass['spcl_attk'],
                       spcl_def=stat_pass['spcl_def'],
                       base_speed=stat_pass['base_speed'],
                       weight=stat_pass['weight'],
                       moves=move_list,
                       moveset=move_list1,
                       level=level)
        return poke

    bag_size = 4
    bag = []

    if args :
        bag_size = args[0]

    for slots in range(bag_size):
        bag.append(create_poke())
    return bag
def game(player,rival):
    def select_player_poke():
        #set plaer current pokemon and current hp
        i = 0
        for x in player:
            i= i + 1
            print('--------------------------------------------')
            sys.stdout.write(str(i))
            x.print_stats()
        choice = int(input('Please Select Entry Number: ')) - 1
        player_current_pokemon = player[choice]
        if player_current_pokemon.hp <=0:
            print('No HP remaining')
            return(player_current_pokemon)
        return (player_current_pokemon)
    def select_rival_poke():
        for poke in rival:
            if poke.hp > 0:
                return (poke)
    def poke_check():
        #check player for gameover
        def player_check():
            global running
            global win_state
            i = 0
            for pokemon in player:
                if pokemon.hp <= 0:
                    i = i + 1
                if pokemon.hp < 0:
                    pokemon.hp = 0
            if i == len(player):
                print('player out of Pokemon, Game Over')
                win_state = False
                running = False
        def rival_check():
            global running
            global win_state
            i = 0
            for pokemon in rival:
                if pokemon.hp <= 0:
                    i = i + 1
                if pokemon.hp < 0:
                    pokemon.hp = 0
            if i == len(rival):
                print('Rival out of pokemon, Game Over')
                win_state = True
                running = False

        player_check()
        rival_check()
    def damage(attacker, attackee, move_power):
        if type(move_power) != int:
            move_power=50
        l = int(attacker.level)
        p = int(move_power)
        a = int(attacker.attack)
        d = int(attackee.defense)
        damage = ((((((2 * l) / 5) + 2) * p * (a / d)) / 50) + 2)
        attackee.hp = attackee.hp - int(damage)
    def turn():
        def get_move_selection():
            if current_player_poke.hp > 0:
                player_movest = current_player_poke.moveset
                #rewrite
                i = 0
                for move in player_movest:
                    i = i +1
                    sys.stdout.write(str(i))
                    sys.stdout.write(': ')
                    sys.stdout.write(str(move))
                    sys.stdout.write('  ')
                choice = int(input('\nPlease select move number: ')) - 1
                return choice
        def player_turn(choice):
            if current_player_poke.hp > 0:
                player_movest = current_player_poke.moveset
                current_player_move = player_movest[choice]
                print('Player used: ', current_player_move[0])
                damage(current_player_poke,current_rival_poke,current_player_move[1])
        def rival_turn():
            if current_rival_poke.hp > 0:
                rival_moveset = current_rival_poke.moveset
                choice = random.randrange(0,3)
                current_rival_move = rival_moveset[choice]
                print('Rival used: ', current_rival_move[0])
                damage(current_rival_poke, current_player_poke, current_rival_move[1])

        choice = get_move_selection()

        if current_player_poke.speed >= current_rival_poke.speed:
            player_turn(choice)
            rival_turn()
        else:
            rival_turn()
            player_turn(choice)

    global running
    global win_state
    running = True
    counter = 0
    current_player_poke = select_player_poke()
    current_rival_poke = select_rival_poke()

    #main loop
    while running:
        counter = counter + 1
        if current_player_poke.hp > 0 and current_rival_poke.hp > 0:
            print('\u001b[1m',current_player_poke.name,' : ',current_player_poke.hp)
            print(current_rival_poke.name, ' : ', current_rival_poke.hp,'\u001b[0m')
            turn()
            print('----------------------------------------------\u001b[0m')
            time.sleep(1)
        elif current_rival_poke.hp <= 0:
            print('Rival dead')
            current_rival_poke = select_rival_poke()
            time.sleep(1)
        elif current_player_poke.hp <= 0:
            print('Player {0} dead'.format(current_player_poke.name))
            current_player_poke = select_player_poke()
            time.sleep(1)
        poke_check()
        clear_console()
        #end loop
    return win_state

if __name__ == '__main__':
    #bag_genorator can be passaed amount of pokemon wanted
    user_bag = bag_generator(4)
    rival_bag = bag_generator(4)

    intro_splash()
    game(user_bag,rival_bag)

    #game(testbag1,testbag2)





