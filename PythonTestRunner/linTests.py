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

def filtrar(texto, archivo, i=0):
	try:
		cadena = "\n"+str(i)+"-"*35+"\n.../"+nombrar(archivo)+":\n"
		texto = texto.split("\n")
		for linea in texto:
			if "Tests run:" in linea:
				cadena += "   -->"+linea+"_"*6
				if "Failures: 0" in linea:
					return cadena+"OK"
				else:
					return cadena+" ERROR!!!!"      
			return cadena + " !FALLO!!"
	except:
		print "Saliendo: 2"
		return ""

def ejecutar(archivos):
	i = 1
	path = cmd("whereis nunit-console")
	path = path.split(" ")[1]
	print ">Path NUnit= "+path+"\n\n"
	for archivo in archivos:
		try:
			salida = cmd("cd .. && "+path+" "+archivo)
			print filtrar(salida, archivo, i)
		except:
			print "Saliendo: 1"
			sys.exit(1)
		i += 1

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

