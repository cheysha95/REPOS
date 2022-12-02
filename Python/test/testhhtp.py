class FoodItem:
    # TODO: Define constructor with arguments to initialize instance 
    #       attributes (name, fat, carbs, protein)
    def __init__(self,**kwargs):
        if kwargs['name'] == 'water':
            self.name = 'Water'
            self.fat = 0.0
            self.carbs = 0.0
            self.protein = 0.0
        else:
            self.name = kwargs['name']
            self.fat = kwargs['fat']
            self.carbs = kwargs['carbs']
            self.protein = kwargs['protein']


    def get_calories(self, num_servings):
        # Calorie formula
        calories = ((self.fat * 9) + (self.carbs * 4) + (self.protein * 4)) * num_servings;
        return calories

    def print_info(self):
        print(f'Nutritional information per serving of {self.name}:')
        print(f'  Fat: {self.fat:.2f} g')
        print(f'  Carbohydrates: {self.carbs:.2f} g')
        print(f'  Protein: {self.protein:.2f} g')


if __name__ == "__main__":

    item_name = input()
    if item_name == 'Water' or item_name == 'water':
        food_item = FoodItem(name='water') #if water pass water as name, constructor takes care of rest
        food_item.print_info()
        print(f'Number of calories for {1.0:.2f} serving(s): {food_item.get_calories(1.0):.2f}')

    else:
        amount_fat = float(input())
        amount_carbs = float(input())
        amount_protein = float(input())
        num_servings = float(input())

        food_item = FoodItem(name= item_name, fat =  amount_fat, carbs = amount_carbs, protein = amount_protein)
        food_item.print_info()
        print(f'Number of calories for {1.0:.2f} serving(s): {food_item.get_calories(1.0):.2f}')
        print(f'Number of calories for {num_servings:.2f} serving(s): {food_item.get_calories(num_servings):.2f}')