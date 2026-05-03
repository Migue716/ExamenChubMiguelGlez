import type { Cliente, ClienteInput } from '../types/cliente'

const apiBase = (import.meta.env.VITE_API_URL as string | undefined)?.replace(/\/$/, '') ?? ''

async function parseError(res: Response): Promise<string> {
  const text = await res.text()
  try {
    const j = JSON.parse(text) as { title?: string; detail?: string }
    return j.detail ?? j.title ?? text
  } catch {
    return text || res.statusText
  }
}

async function request<T>(path: string, init?: RequestInit): Promise<T> {
  const headers: HeadersInit = {
    Accept: 'application/json',
    ...(init?.body ? { 'Content-Type': 'application/json' } : {}),
    ...init?.headers,
  }
  const res = await fetch(`${apiBase}${path}`, { ...init, headers })
  if (!res.ok) throw new Error(await parseError(res))
  if (res.status === 204) return undefined as T
  return (await res.json()) as T
}

export async function listClientes(): Promise<Cliente[]> {
  return request<Cliente[]>('/api/Clientes')
}

export async function searchByNombre(nombre: string): Promise<Cliente[]> {
  const q = new URLSearchParams()
  if (nombre.trim()) q.set('nombre', nombre.trim())
  const suffix = q.toString() ? `?${q}` : ''
  return request<Cliente[]>(`/api/Clientes/search${suffix}`)
}

export async function createCliente(body: ClienteInput): Promise<Cliente> {
  const payload = {
    nombre: body.nombre,
    edad: body.edad,
    direccion: body.direccion,
    correoElectronico: body.correoElectronico,
  }
  return request<Cliente>('/api/Clientes', {
    method: 'POST',
    body: JSON.stringify(payload),
  })
}

export async function updateCliente(id: number, body: ClienteInput): Promise<void> {
  await request<void>(`/api/Clientes/${id}`, {
    method: 'PUT',
    body: JSON.stringify({ ...body, id }),
  })
}

export async function deleteCliente(id: number): Promise<void> {
  await request<void>(`/api/Clientes/${id}`, { method: 'DELETE' })
}
