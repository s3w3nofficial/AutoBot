import RPi.GPIO as GPIO

class Car():


    def __init__(self):
        GPIO.setmode(GPIO.BOARD)

        GPIO.setup(3, GPIO.OUT)
        GPIO.setup(5, GPIO.OUT)
        GPIO.setup(7, GPIO.OUT)
        GPIO.setup(11, GPIO.OUT)

        #set channel=3 frequency=100Hz
        self.a = GPIO.PWM(3, 100)
        self.b = GPIO.PWM(5, 100)
        self.c = GPIO.PWM(7, 100)
        self.d = GPIO.PWM(11, 100)

        self.a.start(0)
        self.b.start(0)
        self.c.start(0)
        self.d.start(0)

    def Ride(self, x, y):
        self.x = x
        self.y = y

        if self.x > 100:
            self.x = 100
        if self.y > 100:
            self.y = 100

        if self.x < -100:
            self.x = -100
        if self.y < -100:
            self.y = -100

        if self.x == 0:
            self.a.ChangeDutyCycle(0)
            self.b.ChangeDutyCycle(0)
        elif self.x > 0:
            self.a.ChangeDutyCycle(self.x)
            self.b.ChangeDutyCycle(0)
        elif self.x < 0:
            self.b.ChangeDutyCycle(abs(self.x))
            self.a.ChangeDutyCycle(0)

        if self.y <= 30 and self.y >= -30:
            self.c.ChangeDutyCycle(0)
            self.d.ChangeDutyCycle(0)
        elif self.y > 30:
            self.c.ChangeDutyCycle(self.y)
            self.d.ChangeDutyCycle(0)
        elif self.y < -30:
            self.d.ChangeDutyCycle(abs(self.y))
            self.c.ChangeDutyCycle(0)

    def Stop(self):
        self.a.stop()
        self.b.stop()
        self.c.stop()
        self.d.stop()
        GPIO.cleanup()
