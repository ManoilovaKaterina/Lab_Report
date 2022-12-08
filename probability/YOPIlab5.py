import numpy as np
import math 
import scipy

def factorial(n):
    fact = 1
    if n == 0:
        return fact
    else:
        for i in range(1, n + 1):
            fact = fact*i
        return fact

def combination(k, n):
    C = factorial(n)/(factorial(k)*factorial(n-k))
    return C

def Bernoulli(k, n, p):
    B = combination(k, n)*pow(p, k)*pow((1-p), (n-k))
    return B

def GaussFunc(x):
    f = (1/math.sqrt(2*math.pi))*pow(math.e, (x**2)/2)
    return f

def MoivreLaplace(k, n, p):
    x = (k-n*p)/math.sqrt(n*p*(1-p))
    f = GaussFunc(x)
    ML = (1/math.sqrt(n*p*(1-p)))*f
    return ML

def Poisson(k, n, p):
    l = n*p
    P = (pow(l, k)/factorial(k))*pow(math.e, -l)
    return P

def IntegralMoivreLaplace(k1, k2, n, p):
    x1 = (k1-n*p)/math.sqrt(n*p*(1-p))
    x2 = (k2-n*p)/math.sqrt(n*p*(1-p))
    if x1<-5:
        f1 = -0.5
    elif x1>5:
        f1 = 0.5
    else:
        if(x1<0):
            f1 = -(scipy.stats.norm.cdf(-x1)-0.5)
        else:
            f1 = scipy.stats.norm.cdf(x1)-0.5
    
    if x2<-5:
        f2 = -0.5
    elif x2>5:
        f2 = 0.5
    else:
        if(x2<0):
            f2 = -(scipy.stats.norm.cdf(-x2)-0.5)
        else:
            f2 = scipy.stats.norm.cdf(x2)-0.5

    ML = f2 - f1
    return ML

def FindInRange(n, p):
    result = 0
    for i in np.arange(n*p-(1-p), n*p+p, 0.01):
        if round(i, 1)%1 == 0.0:
            result = int(round(i, 0))
            break
    return result

def task1():
    print("\nЗавдання 1: ")
    p = 0.2
    trains = 5
    trainswith = 3
    print("Ймовірність того, що в трьох із п’яти потягів будуть вагони на дане призначення.: ", round(Bernoulli(trainswith, trains, p), 4))

def task2():
    print("Завдання 2:")
    n = 5
    p = 0.8
    print("Ймовірність того, що в п’яти незалежних випробуваннях подія А відбудеться:")
    print("а) рівно 4 рази: ", round(Bernoulli(4, n, p), 4))
    print("б) не менше 4 разів: ", round(Bernoulli(4, n, p)+Bernoulli(5, n, p), 4))

def task3():
    print("Завдання 3: ")
    nlp = 0.2
    lollipop = 80
    candies = 400
    print("Ймовірність того, що серед 400 вибраних навмання цукерок буде рівно 80 льодяників: ", round(MoivreLaplace(lollipop, candies, nlp), 4))

def task4():
    print("Завдання 4: ")
    pdef = 0.0001
    autos = 100000
    defective = 5
    print("Ймовірність того, що з конвеєра зійшло 5 бракованих автомобілів: ", round(Poisson(defective, autos, pdef), 4))

def task5():
    print("Завдання 5: ")
    phigh = 0.4
    boots = 600
    highfrom = 228
    highto = 252
    print("Ймовірність того, що серед 600 пар виявиться від 228 до 252 пар взуття вищого ґатунку: ", 
    round(IntegralMoivreLaplace(highfrom, highto, boots, phigh), 4))

def task6():
    print("Завдання 6: ")
    clients = 100
    p = 0.4
    pmost = FindInRange(clients, p)
    print("Найімовірніше число вимог клієнтів кожного дня: ", pmost)
    print("його ймовірність: ", round(MoivreLaplace(pmost, clients, p), 4))

def task7():
    print("Завдання 7: ")
    pnotstan = 0.04
    n = 4000
    maxnotstan = 170
    print("Ймовірність, що число нестандартних виробів у партії з 4000 штук не більше 170: ", round(IntegralMoivreLaplace(0, maxnotstan, n, pnotstan), 4))

def task8():
    print("Завдання 8: ")
    pnotstan = 0.5
    n = 10000
    maxnotstan = 5000
    print("Ймовірність, що при 10000 незалежних киданнях монети герб випаде 5000 разів: ", round(MoivreLaplace(maxnotstan, n, pnotstan), 5))

def task9():
    print("Завдання 9: ")
    qual = 1000
    damaged = 5
    pdamage = 0.002
    print("Ймовірність того, що на базу прибуде 5 пошкоджених виробів: ", round(Poisson(damaged, qual, pdamage), 4))

def task10():
    print("Завдання 10: ")
    pmistake = 0.03
    money = 150
    print("Найімовірніше число випадків правильної роботи автомату, якщо буде кинуто 150 монет: ", FindInRange(money, 1-pmistake)) 

task1()
print('-' * 100)
task2()
print('-' * 100)
task3()
print('-' * 100)
task4()
print('-' * 100)
task5()
print('-' * 100)
task6()
print('-' * 100)
task7()
print('-' * 100)
task8()
print('-' * 100)
task9()
print('-' * 100)
task10()