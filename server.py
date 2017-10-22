import socket, os, sys, time
from lib.car import *

if __name__ == "__main__":
    car = Car()
    s = socket.socket(socket.AF_INET,socket.SOCK_STREAM)

    s.bind(("0.0.0.0",5901))
    s.listen(1)

    while True:

        connect, address = s.accept()
        if connect:
            while True:
                recv = (connect.recv(1024)).strip()
                print (str(recv) + " " + str(address))
                tmp = recv.split(',')
                print tmp
                car.Ride(int(tmp[0]), int(tmp[1]))
                resp = "ok"
                connect.send(resp)

        connect.close()
    """
    car.Ride(100, 70)
    time.sleep(10)
    car.Stop()
    """
