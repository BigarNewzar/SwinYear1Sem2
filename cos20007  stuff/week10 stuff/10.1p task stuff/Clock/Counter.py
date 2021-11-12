class Counter(object):
    
    def __init__(self, name):
         self.__name = name
         self.__count = 0

    def Increment(self):
        self.__count +=1

    def Reset(self):
        self.__count = 0

    def get_name(self):
        return self.__name

    def set_name(self, value):
        self.__name = value

    def get_ticks(self):
        return self.__count

    

    


