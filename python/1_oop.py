class Person:
    # class attributes
    address = 'Selimpaşa'
    # constructor
    # önce self geliyo constructorda sonra diğer parametreler
    def __init__(self, name, year, addr):
        self.name = name
        self.birthyear = year
        self.address = addr
    # fonksiyon tanımlama
    def metod(self):
        print('Hello ' + self.address)
    

p1 = Person('Ermand', 15, 'Silvri') # obje oluşturma
p2 = Person(name='Emre', year=2002, addr='Silvri') # obje oluşturma

print(p1.name)
print(p1.birthyear)
print(p1.address)

p1.metod()

