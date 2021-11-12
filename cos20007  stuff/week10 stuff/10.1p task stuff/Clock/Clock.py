class Clock(object):
    __sec = Counter()
    __min = Counter()
    __hr = Counter()
    def __init__(self__sec,self__min,self__hr):
         self__sec = Counter()
         self__min = Counter()
         self__hr = Counter()

    def tick(self__sec,self__min,self__hr):
         self__sec = Counter().Increment
         if(self__sec.Counter().get_ticks >59):
            self__sec = Counter().Reset
            self__min = Counter().Increment
         
         if(self__min.Counter().get_ticks >59):
            self__min = Counter().Reset
            self__hr = Counter().Increment
         if(self__hr.Counter().get_ticks >23):
            self__hr = Counter().Reset
     
    def PrintTime(self__sec,self__min,self__hr):
          return String.Format("{0:D2}", self__hr) + ":" + String.Format("{0:D2}", self__min) + ":" + String.Format("{0:D2}", self__sec);
    
    def Reset(self__sec,self__min,self__hr):
          self__sec = Counter().Reset
          self__min = Counter().Reset
          self__hr = Counter().Reset


