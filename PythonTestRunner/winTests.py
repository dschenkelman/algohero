import sys, os
try:
	import popen2
except:
	pass

def cmd(comando):
	f = popen2.popen3(comando)
	try:
		return f[0].read()
	except:
		print "Imposible de ejecutar Comando: <"+comando+">"
		print 
		raw_input("Nos vemos luego, enter para salir")
		sys.exit(1)

def cargar():
	path = cmd("cd")[:-1]
	directoriosTest = cmd("cd Source && dir /b *.Tests")
	archivos = []
	directoriosTest = directoriosTest.split("\n")
	for directorio in directoriosTest:
		salida = cmd("cd \"Source/"+directorio+"/bin/Debug/\" && dir /b *Tests.dll")
		salida = salida.split("\n")
		for archivoTest in salida:
			if "Test" in archivoTest:
				absoluto = path+"/Source/"+directorio+"/bin/Debug/"+archivoTest
				print ">Encontrado: "+archivoTest
				archivos.append(absoluto)
	return archivos

def nombrar(cadena):
	cadena = cadena[22:]
	cadenas = cadena.split("/")
	final = ""
	for cad in cadenas:
		if (cad == "bin") or (cad == "Debug"):
			final += "../"
		else:
			final += cad+"/"
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

def ejecutar(archivos):
	i = 1
	for archivo in archivos:
		try:
			salida = cmd("cd .. && "*4+" cd Arch* && cd *2.4.8 && cd bin && nunit-console.exe \""+archivo+"\"")
			print filtrar(salida, archivo, i)
		except:
			print "Saliendo: 1"
			sys.exit(1)
		i += 1

def main():
	os.system("cls")
	print "--_Bienvenidos_--"
	print "\nSe estan inciando y buscando los tests...\n"
	tests = cargar()
	print "\n",len(tests),"Tests Detectados;\nCorriendo los Tests: aguarde...\n"
	ejecutar(tests)
	raw_input("\n\nTERMINADO! (enter para salir)")
	

if __name__ == "__main__":
	main()
