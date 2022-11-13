import os
import pokemon
import requests

class map_grid:
        def __init__(self, width, height, char,description):
            self.width = width
            self.height = height
            self.map_features = [] # every object on map will be added to this list, and collision will look at thair locatioin and see if player is on one
            self.char = char # symbol for floor
            self.description = description # words
class game_object:
        def __init__(self,x , y, char, name):
            self.cord = (x,y)
            self.char = char # symbol for object
            self.name = name
class wall(game_object):
    def __init__(self, x, y, x2, y2):
        self.x2 = x2
        self.y2 = y2
        self.x = x
        self.y = y
        super().__init__(x=self.x, y=self.y, char='#', name='wall')
class player(game_object):
        def __init__(self,x,y):
            super().__init__(x,y,char='@',name='player')
            self.pokebag = pokemon.bag_generator(2)
        hp = 10
class enemy(game_object):
        def __init__(self,x,y,char,name):
            super().__init__(x,y,char,name)
        hp = 10
        damage = 1
class npc(game_object):
    def __init__(self, x, y):
        super().__init__(x, y, char='$', name='npc')
        self.pokebag = pokemon.bag_generator(2)
        self.is_active = True
    def text_gen(self):
        client = requests.get('https://www.boredapi.com/api/activity')
        client.close()
        response = client.json()
        response = response['activity']
        text = 'you should totally ' + response
        print(text)
        input()
        return text
class hazard(game_object):
        def __init__(self,x,y,char,name):
            super().__init__(x,y,char,name)
        damage = 1
class map_exit(game_object):
        def __init__(self,x,y):
            self.x = x
            self.y = y
            super().__init__(x=self.x,y=self.y,char='∩',name='map_exit')
class map_prev(game_object):
    def __init__(self, x, y):
        self.x = x
        self.y = y
        super().__init__(x=self.x, y=self.y, char='U', name='map_prev')
class spawn(game_object):
        def __init__(self,x,y):
            self.x = x
            self.y = y
            super().__init__(x=self.x,y=self.y,char='_',name='spawn')
def clear_console():
        os.system('cls')
def draw_grid(graph, width=2):
        for y in range(graph.height):
            for x in range(graph.width):
                char = graph.char
                for object in object_list:
                    if (x,y) == object.cord and object.name != 'spawn':
                        char = object.char
                    if type(object) == wall and x >= object.x2 and x <= object.cord[0] and y >= object.y2 and y <= object.cord[1]:
                        char = object.char
                print("%%-%ds" % width % char, end="")
            print()
def get_input():
        choice = str.lower(input('what do you want to do? : W, A, S, D  '))
        return choice
def player_update(choice):
        def collision():
            global map_counter, current_map, player
            for game_object in object_list:
                # defining what happens when you collide with game_ojects,
                # turns off collision for , player, spawn
                if player.cord == game_object.cord and type(game_object) != type(player) and type(game_object) !=spawn:
                    if type(game_object) == hazard or type(game_object) == enemy: #damage
                        input('you hit a hazard')
                        player.hp = player.hp - game_object.damage
                        return True
                    if type(game_object) == npc:
                        game_object.text_gen()
                        if game_object.is_active:
                            game_object.is_active = False
                            clear_console()
                            pokemon.game(player.pokebag,game_object.pokebag)
                    if type(game_object) == map_exit:
                        map_counter = map_counter + 1
                        current_map = map_list[map_counter]
                        player.cord = (current_map.map_features[0].x, current_map.map_features[0].y)
                    if type(game_object) == map_prev:
                        map_counter = map_counter - 1
                        current_map = map_list[map_counter]
                        player.cord = (current_map.map_features[0].x, current_map.map_features[0].y)
                    return True
                # collision with walls
                if type(game_object) == wall and player.cord[0] >= game_object.x2 and player.cord[0] <= game_object.cord[0] and player.cord[1] >= game_object.y2 and player.cord[1] <= game_object.cord[1]:
                    return True

        if choice == str.lower('w'):
            player.cord = (player.cord[0], player.cord[1]-1)
            if collision() or player.cord[1] < 0:           # if collision or boundries, put player back
                player.cord = (player.cord[0], player.cord[1]+1)
        elif choice == str.lower('s'):
            player.cord = (player.cord[0], player.cord[1] + 1)
            if collision() or player.cord[1] > current_map.height-1:
                player.cord = (player.cord[0], player.cord[1] - 1)
        elif choice == str.lower('d'):
            player.cord = (player.cord[0] + 1, player.cord[1])
            if collision() or player.cord[0] > current_map.width-1:
                player.cord = (player.cord[0] - 1, player.cord[1])
        elif choice == str.lower('a'):
            player.cord = (player.cord[0] - 1, player.cord[1])
            if collision() or player.cord[0] < 0:
                player.cord = (player.cord[0] + 1, player.cord[1])
        else:
            print('invalid')
            player_update(get_input())
def enemy_update(): # not used
        for game_object in object_list:
            if type(game_object) == enemy:
                game_object.cord = (game_object.cord[0] - 1 ,game_object.cord[1])
                if game_object.cord == player.cord:
                    game_object.cord = (game_object.cord[0] + 1, game_object.cord[1])
                    player.hp = player.hp - game_object.damage
                if game_object.cord[0] < 0 :
                    game_object.cord = (game_object.cord[0] + 1, game_object.cord[1])

print('please wait while we set up')

map1 = map_grid(50, 20, '.','mountain room')
map1.map_features = [spawn(x=1,y=1), map_exit(x=20,y=2), map_prev(x=-1,y=-1)] + [wall(10,10,10,3)] + [wall(10,1,3,1)] + [npc(4,5)] + [enemy(12,12,'*','fucker')]

map2 = map_grid(20,5, '_','A Narrow Roadway')
map2.map_features = [spawn(x=4,y=1), map_exit(x=10,y=2), map_prev(x=4,y=3)]

map3 = map_grid(29,4, '^','A River')
map3.map_features = [spawn(x=1,y=1), map_exit(x=10,y=2), map_prev(x=4,y=3)]

map_list = [map1, map2, map3]
wallpaper = r'''          /\
         /**\
        /****\   /\
       /      \ /**\
      /  /\    /    \        /\    /\  /\      /\            /\/\/\  /\
     /  /  \  /      \      /  \/\/  \/  \  /\/  \/\  /\  /\/ / /  \/  \
    /  /    \/ /\     \    /    \ \  /    \/ /   /  \/  \/  \  /    \   \
   /  /      \/  \/\   \  /      \    /   /    \
__/__/_______/___/__\___\__________________________________________________'''
counter = 0
map_counter = 0 # keeps track of current room
current_map = map_list[map_counter]
player = player(current_map.map_features[0].x,current_map.map_features[0].y) #spawns player on spawn
clear_console()

while player.hp > 0: # main game loop
    #update object_list
    object_list = [player] + current_map.map_features
    #draw map and contents
    print('Hp: ', player.hp)
    print(wallpaper)
    draw_grid(current_map)
    print(current_map.description)
    #player input, update cords
    player_update(get_input())
    clear_console()
    counter = counter + 1
    enemy_update()







