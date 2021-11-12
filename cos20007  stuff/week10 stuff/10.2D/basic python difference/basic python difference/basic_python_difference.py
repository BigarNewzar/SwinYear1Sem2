class Number:
    # instance attribute
    def __init__(self, _num):
        self.number = _num

    # instance method
    def Addself(self):
        self.number = self.number + self.number

    # instance method
    def Print(self):
        return self.number

# instantiate the class
num = Number(45)
# call our instance methods
num.Addself()
print(num.Print())

# instantiate the class
num = Number("45 ")
num.Addself()

print(num.Print())
