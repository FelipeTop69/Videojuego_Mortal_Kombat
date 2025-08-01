

export interface PedidoMod{
    id : number,
    fecha: Date,
    estado: string,
    ClienteNombre: string,
    PizzaNombre:string

    empleadoId : number,
    proyectoId: number,
}

export interface PedidoCreateMod{
    id : number,

    clienteId : number,
    clienteName : string,

    pizzaId: number,
    pizzaName: string,
}

export interface PedidoUpdateMod{
    id : number,

    pedidoId : number,
    nuevoEstado : string,
}
