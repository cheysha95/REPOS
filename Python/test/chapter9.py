class Artist:
    def __init__(self, name = 'unknown', birth_year = -1, death_year = -1 ):
        self.name = name
        self.birth_year = birth_year
        self.death_year = death_year
    def print_info(self):
        if self.death_year and self.birth_year <= 0:
            date = '({0} to {1})'.format(self.birth_year,self.death_year)
        elif self.birth_year <= 0 and self.death_year >= 0:
            date = '({0} to present)'.format(self.birth_year)
        else:
            date = '(unknown)'

        print(self.name, date )

class Artwork:
    def __init__(self, title = 'unknown', year_created = -1, artist = -1):
        self.title = title
        self.year_created = year_created
        self. artist = artist
    def print_info(self):
        print('Title: ',self.title, ',', self.year_created)

if __name__ == "__main__":
    user_artist_name = input()
    user_birth_year = int(input())
    user_death_year = int(input())
    user_title = input()
    user_year_created = int(input())

    user_artist = Artist(user_artist_name, user_birth_year, user_death_year)

    new_artwork = Artwork(user_title, user_year_created, user_artist)

    user_artist.print_info()
    new_artwork.print_info()