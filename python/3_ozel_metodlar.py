class Movie:
    name = 'InterStellar'
    category = 'Sci-Fi'
    # str fonksiyonunun içine alındığında ne döndüreceğini buradan belirtebiliyorum C# taki ToString() gibi
    def __str__(self):
        return self.name + ' ' + self.category

    def __len__(self):
        return len(self.name)
    # del metodu çalıştığında destructor fonksiyonu
    def __del__(self):
        print('Movie destructor called')

m = Movie()

print(str(m))
print(len(m))

del m