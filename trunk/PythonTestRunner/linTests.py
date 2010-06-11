import sys, os, popen2

def cmd(comando):
	out = popen2.popen3(comando)
	try:
		return out[0].read()
	except:
		print "Imposible de ejecutar Comando: <"+comando+">"
		print 
		raw_input("Nos vemos luego, enter para salir")
		sys.exit(1)

def cargar():
	directoriosTest = cmd("cd .. && ls Source/*Tests/bin/Debug/*.Tests.dll")
	archivos = []
	directoriosTest = directoriosTest.split("\n")
	for archivo in directoriosTest:
		print ">Encontrado: "+archivo
		archivos.append(archivo)
	return archivos

def nombrar(cadena):
	cadenas = cadena.split("/")
	final = ""
	escribir = False
	for cad in cadenas:
		if escribir:
			if (cad == "bin") or (cad == "Debug"):
				final += "../"
			else:
				final += cad+"/"
		if cad == "Source":
			escribir = True
	return final[:-1]

def contar(cadena, num):
	try:
		l = cadena.split(',')
		k = l[num].split(':')
		return int(k[1])
	except:
		return 0


def filtrar(texto, archivo, i=0):
	ok__ = 0
	error__ = 0
	not__ = 0
	try:
		cadena = "\n"+str(i)+"-"*35+"\n.../"+nombrar(archivo)+":\n"
		texto = texto.split("\n")
		for linea in texto:
			if "Tests run:" in linea:
				ok__ += contar(linea, 0)
				error__ += contar(linea, 1)
				not__ += contar(linea, 2)
				cadena += "   -->"+linea+"_"*6
				if "Failures: 0" in linea:
					cadena += "OK"
					l = [ok__, error__, not__, cadena]
					return l
				else:
					os.system("color C && echo ")
					cadena += "ERROR!!!"
					l = [ok__, error__, not__, cadena]
					return l

		os.system("color C && echo ")
		cadena += "__FAIL__ !!"
		l = [ok__, error__, not__, cadena]
		return l
	except:
		print "Saliendo: 2"
		return [0,0,0,""]

def ejecutar(archivos):
	i = 1
	ok_ = 0
	error_ = 0
	not_c = 0
	path = cmd("whereis nunit-console")
	path = path.split(" ")[1]
	print ">Path NUnit= "+path+"\n\n"
	for archivo in archivos:
		try:
			salida = cmd("cd .. && "+path+" "+archivo)
			l = filtrar(salida, archivo, i)
			ok_ += l[0]
			error_ += l[1]
			not_c += l[2]
			print l[3]
		except:
			print "Saliendo: 1 (enter)"
			raw_input("")
			sys.exit(1)
		i += 1
	print
	print "Estado Final: "
	print "     -Corridos:", ok_
	print "     -Corridos y Fallados:", error_
	print "     -No Corridos:", not_c

def ayuda():
	print "inserte ayuda aqui"

def main():
	tests = cargar()
	print "\n",len(tests),"Tests Detectados;\nCorriendo los Tests: aguarde...\n"
	ejecutar(tests)
	help = raw_input("\n\nTERMINADO! (enter para salir; help y enter para ayuda)")
	if "help" in help:
		ayuda()
		raw_input("\nFIN! (enter para salir)")
        

if __name__ == "__main__":
	os.system("clear")
	print "--_Bienvenidos_--"
	print "\nInciando ambiente y buscando tests...\n\n"
	main()

