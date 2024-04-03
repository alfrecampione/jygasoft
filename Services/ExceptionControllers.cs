namespace Services;

public class ExceptionControllers:Exception
{
    public ExceptionControllers(string message, Exception inner):base(message,inner)
    {
    }
}
/*
 400 Solicitud incorrecta: el servidor no entendió la solicitud. 
401 No autorizado: no se proporcionó la autenticación requerida. 
402 Pago requerido: actualmente no está en uso. 
403 Prohibido: el navegador no tiene permiso para realizar esa solicitud. 
404 No encontrado: la página que está buscando no existe o no se puede encontrar. 
405 Método no permitido: el tipo de solicitud no está autorizado o no es compatible. 
406 No aceptable: el servidor no puede proporcionar los datos en el formato solicitado. 
407 Se requiere autenticación de proxy: la solicitud primero debe ser autenticada por un proxy. 
408 Tiempo de espera de solicitud: el servidor no recibió la solicitud de manera oportuna. 
409 Conflicto: los recursos del servidor que se solicitan tienen diferentes versiones o múltiples copias. 
410 Gone: la página solicitada no existe y se eliminó manualmente. 
*/
public enum ErrorsType
{

SolicitudIncorrect=400,

    
        
        
};