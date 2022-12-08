import matplotlib.pyplot as plt
import time
import numpy as np
import math 
import os

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

def task1():
    print("\nЗавдання 1: ")
    black = 40
    brown = 26
    red = 22
    blue = 12
    probability = (red+blue)/(black+brown+red+blue)
    print("Ймовірність того, що навмання взята коробка виявиться із взуттям червоного або синього кольору: ", probability)

def task2():
    print("Завдання 2:")
    twofromall = combination(2, 10)
    twofromnotcons = combination(2, 2)
    PnotA = twofromnotcons/twofromall
    PA = 1 - PnotA
    print("Ймовірність того, що серед навмання вибраних двох співробітників, хоча б один буде консультантом: ", round(PA, 3))

def task3():
    print("Завдання 3: ")
    cthreeten = combination(3, 10)
    cthreenotrel = combination(3, 8)
    PnotA =cthreenotrel/cthreeten
    PA = 1 - PnotA
    print("Ймовірність того, що серед вибраних фахівців буде принаймні один із родичів: ", round(PA, 3))

def task4():
    print("Завдання 4: ")
    p1 = 0.15
    p2 = 0.25
    p3 = 0.2
    p4 = 0.1
    p5 = 1 - (p1+p2+p3+p4)
    print("Ймовірність р5 того, що цей товар призначений для п’ятого відділу: ", round(p5, 1))

def task5():
    print("Завдання 5: ")
    trains = 80
    kol = 120
    p = combination(2, trains)/combination(2, kol)
    print("Знайти ймовірність прибуття двох розбіркових потягів по двох сусідніх коліях: ", round(p, 3))

def task6():
    print("Завдання 6: ")
    pstand = 0.9
    phigh = 0.8
    print("Ймовірність виготовлення виробу першого ґатунку даним станком: ", round(pstand*phigh, 3))

def task7count(st, allst, knownq, questions):
    result = (st/allst)*(knownq/questions)*((knownq-1)/(questions-1))*((knownq-2)/(questions-2))
    return result

def task7():
    print("Завдання 7: ")
    A = 3
    B = 4
    C = 2
    D = 1
    AQ = 20
    BQ = 16
    CQ = 10
    DQ = 5
    ThreeForA = task7count(A, 10, AQ, 20)
    ThreeForB = task7count(B, 10, BQ, 20)
    ThreeForC = task7count(C, 10, CQ, 20)
    ThreeForD = task7count(D, 10, DQ, 20)
    P = ThreeForA+ThreeForB+ThreeForC+ThreeForD
    print("Ймовірність, що даний студент підготовлений відмінно: ", round(ThreeForA/P, 3))
    print("Ймовірність, що даний студент підготовлений погано: ", round(ThreeForD/P, 4))

def countp(c, p):
    result = 0.0
    for i, j in zip(c, p):
        result += i*j
    return result

def task8():
    print("Завдання 8: ")
    details = [0.4, 0.3, 0.3]
    ps = [0.9, 0.95, 0.95]
    p = countp(details, ps)
    print("Ймовірність того, що навмання взята деталь стандартна: ", round(p, 3))

def task9():
    print("Завдання 9: ")
    perit = 0.3
    pperit = 0.7
    illnes = [0.4, perit, 0.3]
    precover = [0.8, pperit, 0.85]
    p = countp(illnes, precover)
    print("Ймовірність того, що виписаний пацієнт був хворий на перитоніт: ", round((perit*pperit)/p, 3))

def task10():
    print("Завдання 10: ")
    highqual = 0.3
    avqual = 0.7
    highn = 0.9
    avn = 0.8
    p = countp([highqual, avqual], [highn, avn])
    print("Ймовірність того, що вибраний предмет, який є надійним, зібраний фахівцем високої кваліфікації: ", round((highqual*highn)/p, 3)) 

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