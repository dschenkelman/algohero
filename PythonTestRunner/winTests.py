import sys, os
try:
	import popen2
except:
	pass

def cmd(comando):
	if "find" in comando:
		print "\nBuscando, directorio NUnit >> TOME NOTA Y MODIFIQUE SU SCRIPT\n"
	f = popen2.popen3(comando)
	try:
		return f[0].read()
	except:
		print "Imposible de ejecutar Comando: <"+comando+">"
		print 
		raw_input("Nos vemos luego, enter para salir")
		sys.exit(1)

def cargar():
	path = cmd("cd .. && cd")[:-1]
	print path
	directoriosTest = cmd("cd.. && cd Source && dir /b *.Tests")
	archivos = []
	directoriosTest = directoriosTest.split("\n")
	for directorio in directoriosTest:
		salida = cmd("cd.. && cd \"Source/"+directorio+"/bin/Debug/\" && dir /b *Tests.dll")
		salida = salida.split("\n")
		for archivoTest in salida:
			if "Test" in archivoTest:
				absoluto = path+"/Source/"+directorio+"/bin/Debug/"+archivoTest
				print ">Encontrado: "+archivoTest
				archivos.append(absoluto)
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

		if cad == "Sources":
			escribir = True
	return final

def filtrar(texto, archivo, i=0):
	try:
		cadena = "\n"+str(i)+"-"*35+"\n..."+nombrar(archivo)+":\n"
		texto = texto.split("\n")
		for linea in texto:
			if "Tests run:" in linea:
				cadena += "   -->"+linea+"_"*6
				if "Failures: 0" in linea:
					return cadena+"OK"
				else:
					return cadena+" ERROR!!!!"	
		return cadena + "FALLO"
	except:
		print "Saliendo: 2"
		return ""

def ejecutar(archivos,PATHN):
	i = 1
	for archivo in archivos:
		try:
			salida = cmd("cd .. && "*20+"\""+PATHN+"\" \""+archivo+"\"")
			print filtrar(salida, archivo, i)
		except:
			print "Saliendo: 1"
			sys.exit(1)
		i += 1

def ayuda():
	print "Si no funciona puede deberse al path de nunit"
	print "revise y modifique la variable PATHN en la 4ta linea"
	print "desde el final del documento; o descomente la 2da "
	print "(desde el final) para realizar una busqueda;"
	print "WARNING: esto puede demorar varios minutos, se aconseja "
	print "tomar nota del path y modificarlo para futuras ejecuciones)"

def main(PATHN):
	print ">Path nunit= "+PATHN+"\n\n"
	tests = cargar()
	print "\n",len(tests),"Tests Detectados;\nCorriendo los Tests: aguarde...\n"
	ejecutar(tests, PATHN)
	help = raw_input("\n\nTERMINADO! (enter para salir; help y enter para ayuda)")
	if "help" in help:
		ayuda()
	

if __name__ == "__main__":
	os.system("cls")
	print "--_Bienvenidos_--"
	print "\nInciando ambiente y buscando tests...\n\n"
	
	# ATENCIOOOONN!!!
	#Si no funciona puede deberse al path de nunit, insertelo aqui! NO SE OLVIDE DE ESCAPEAR LAS BARRAS INVERTIDAS...

	PATHN = "c:\\Archivos de programa\\NUnit 2.4.8\\bin\\nunit-console.exe"
	#Si no lo sabe corra descomente la siguiente linea (ADVERTENCIA: esto puede tardar varios minutos)
	#PATHN = cmd("dir c:\ /s /b | find \"nunit-console.exe\" ").split("\n")[0]
	main(PATHN)
	
