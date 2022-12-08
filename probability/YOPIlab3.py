import matplotlib.pyplot as plt
import time
import numpy as np
import math 
import os

def getArrays(filestr):
    file = open(filestr).read().split('\n')[1:]
    arrayx = []
    arrayy = []
    for item in file:
        splitarr = item.split('\t')
        if splitarr[0] != '':
            arrayx.append(float(splitarr[0].replace(',','.')))
            arrayy.append(int(splitarr[1]))
    return [arrayx, arrayy]

def findMean(arr):
    mid = float(0)
    for i in arr:
        mid = mid + i
    mid = mid/len(arr)
    return mid

def variance(arr):
    sum = 0
    mid = findMean(arr)
    for el in arr:
        sum += (int(el)-mid)**2
    var = sum/len(arr)
    return var

def covariance(arrx, arry):
    sum = 0
    meanx = findMean(arrx)
    meany = findMean(arry)
    for elx, ely in zip(arrx, arry):
        sum += (elx-meanx)*(ely-meany)
    cov = sum/len(arrx)
    return cov

def corCoef(arrx, arry):
    sx = math.sqrt(variance(arrx))
    sy = math.sqrt(variance(arry))
    meanx = findMean(arrx)
    meany = findMean(arry)
    sum = 0
    for elx, ely in zip(arrx, arry):
        sum += ((elx-meanx)/sx)*((ely-meany)/sy)
    r = sum/(len(arrx)-1)
    return r

def linreg(arrx, arry):
    b1 = covariance(arrx, arry)/variance(arrx)
    b0 = findMean(arry)-(b1*findMean(arrx))
    return [b0, b1]

def scatter(arrx, arry):
    b0, b1 = linreg(arrx, arry)
    y = [b1*am+b0 for am in arrx]

    xG = [findMean(arrx)]
    yG = [findMean(arry)]

    ccoef = round(corCoef(arrx, arry), 3)

    plt.figure(facecolor='white', num ='Scatter diagram')
    plt.title("Діаграма розсіювання та лінія регресії")

    if b1>0:
        plt.text(1.3 , 70, 'Тренд є позитивним', fontsize=9)
    else:
        plt.text(1.3 , 70, 'Тренд є негативним', fontsize=9)
    plt.text(1.3 , 64, 'Коефіцієнт кореляції = %s'%(ccoef), fontsize=9)
    plt.text(1.3 , 55, 'Між даними існує позитивна \nлінійна залежність', fontsize=9)

    plt.plot(arrx, y, color='#E06C9F', label = "Лінія регресії")
    plt.scatter(arrx, arry, color = "#393D3F")
    plt.plot(xG, yG, marker="o", markersize=10, markeredgecolor="#E06C9F", markerfacecolor="#EDBFB7",
    label = "Центр ваги (%s)"%(str(round(xG[0], 3))+"; "+str(yG[0])))
    plt.legend(loc = 'best')
    plt.savefig('C:/Users/undor/OneDrive/Рабочий стол/ЙОПІ/task_03_data/scatter.png')

def scenario(file):
    start_time = time.time()

    arrx = getArrays(file)[0]
    arry = getArrays(file)[1]
    scatter(arrx, arry)
    print("--- час виконання: %s секунд ---" % (time.time() - start_time))
    plt.show()

def menu():
    print("Введіть кількість елементів файлу (10, 100): ", end=" ")
    comm = input()
    if comm == "10" or comm == "100":
        filestring = "C:/Users/undor/OneDrive/Рабочий стол/ЙОПІ/task_03_data/input_" + comm +".txt"
        scenario(filestring)
    else:
        print("Введеної кількості елементів не існує, спробуйте ще: ")
        menu()

menu()