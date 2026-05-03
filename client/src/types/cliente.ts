export interface Cliente {
  id: number
  nombre: string
  edad: string
  direccion: string
  correoElectronico: string
}

export type ClienteInput = Omit<Cliente, 'id'> & { id?: number }
