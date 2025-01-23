Actividad práctica “El dador de vida”
Autor: Jonathan Albert

Se realizo la creación de un juego de combate, donde 2 jugadores se enfrentan en una area para eliminar al otro jugador o conseguir puntos enfrentandose a un robot gigante.

2 Jugadores de forma simultanea

Parte del reto en la actividad es tener a 2 jugadores en la pantalla con un sistema de input independiente para cada uno, esto se logro manejando 2 esquemas diferentes con el nuevo sistema de input. Para el jugador 1 se utiliza el esquema de teclado y raton. Para el jugador 2 se utiliza el esquema de un gamepad. Esto permite que ambos jugadores puedan jugar de forma simultanea.

Camara dividida

Como parte de la actividad se pide manejar la camara dividida para que ambos jugadores puedan jugar en la misma pantalla, esto se hizo manejando el viewport react de cada una de las camaras de los jugadores

Cambio de camaras

En la actividad se solicita que los jugadores puedan cambiar entre tercera y primera persona. Esto se realizo colocando una camara adicional en primera persona para cada uno de los jugadores y ajustando el sistema de input para colocar una accion que controle el cambio de camara. El cambio de camara del jugador 1 se realiza con la tecla “Espacio”, el cambio de camara del jugador 2 se realiza con el “Bumper Izquierdo”.

Mecanica

Se tiene como mecanica principal el disparo de projectiles, lo que hago es poner un punto de disparo delante del arma y desde ese punto hago la instancia de los prefabs de los projectiles.

Objetivo

El objetivo del juego es eliminar al otro jugador o llegar hasta los 100 puntos, si disparamos al otro jugador vamos a quitarle vida pero sin ganar puntos, los puntos se obtienen solo cuando nos enfrentamos al robot gigante que esta dando vueltas al escenario y le disparamos en la caja verde que posee en la parte inferior, el robot tambien nos puede disparar y quitarnos vida. Si algun jugador completa alguna de las condiciones de victoria se visualiza un panel indicando la victoria y la derrota de ambos jugadores.
