import { useCallback, useEffect, useState } from 'react'
import * as api from './api/clientesApi'
import type { Cliente, ClienteInput } from './types/cliente'
import './App.css'

const emptyForm: ClienteInput = {
  nombre: '',
  edad: '',
  direccion: '',
  correoElectronico: '',
}

function App() {
  const [clientes, setClientes] = useState<Cliente[]>([])
  const [form, setForm] = useState<ClienteInput>(emptyForm)
  const [editingId, setEditingId] = useState<number | null>(null)
  const [searchNombre, setSearchNombre] = useState('')
  const [loading, setLoading] = useState(false)
  const [flash, setFlash] = useState<{ type: 'ok' | 'err'; text: string } | null>(null)

  const showError = (err: unknown) => {
    setFlash({
      type: 'err',
      text: err instanceof Error ? err.message : 'Error desconocido',
    })
  }

  const loadAll = useCallback(async () => {
    setLoading(true)
    setFlash(null)
    try {
      setClientes(await api.listClientes())
    } catch (e) {
      showError(e)
    } finally {
      setLoading(false)
    }
  }, [])

  useEffect(() => {
    void loadAll()
  }, [loadAll])

  const resetForm = () => {
    setForm(emptyForm)
    setEditingId(null)
  }

  const selectRow = (c: Cliente) => {
    setEditingId(c.id)
    setForm({
      id: c.id,
      nombre: c.nombre,
      edad: c.edad,
      direccion: c.direccion,
      correoElectronico: c.correoElectronico,
    })
  }

  const handleSearch = async () => {
    setLoading(true)
    setFlash(null)
    try {
      setClientes(await api.searchByNombre(searchNombre))
    } catch (e) {
      showError(e)
    } finally {
      setLoading(false)
    }
  }

  const handleSave = async () => {
    setLoading(true)
    setFlash(null)
    try {
      if (editingId != null) {
        await api.updateCliente(editingId, { ...form, id: editingId })
        setFlash({ type: 'ok', text: 'Cliente actualizado.' })
      } else {
        await api.createCliente(form)
        setFlash({ type: 'ok', text: 'Cliente creado.' })
      }
      resetForm()
      await loadAll()
    } catch (e) {
      showError(e)
    } finally {
      setLoading(false)
    }
  }

  const handleDelete = async () => {
    if (editingId == null) {
      setFlash({ type: 'err', text: 'Selecciona un cliente en la tabla para eliminar.' })
      return
    }
    if (!window.confirm('¿Eliminar este cliente?')) return
    setLoading(true)
    setFlash(null)
    try {
      await api.deleteCliente(editingId)
      setFlash({ type: 'ok', text: 'Cliente eliminado.' })
      resetForm()
      await loadAll()
    } catch (e) {
      showError(e)
    } finally {
      setLoading(false)
    }
  }

  return (
    <div className="app">
      <header className="header">
        <h1>Clientes</h1>
        <p className="subtitle">API .NET · cliente React (búsqueda solo por nombre)</p>
      </header>

      {flash && (
        <div className={`banner ${flash.type === 'ok' ? 'ok' : 'err'}`}>{flash.text}</div>
      )}

      <section className="panel">
        <h2>Formulario</h2>
        <div className="form-grid">
          <label>
            Id
            <input value={editingId ?? ''} readOnly placeholder="(nuevo)" />
          </label>
          <label>
            Nombre
            <input
              value={form.nombre}
              onChange={(e) => setForm((f) => ({ ...f, nombre: e.target.value }))}
            />
          </label>
          <label>
            Edad
            <input
              value={form.edad}
              onChange={(e) => setForm((f) => ({ ...f, edad: e.target.value }))}
            />
          </label>
          <label className="span-2">
            Dirección
            <input
              value={form.direccion}
              onChange={(e) => setForm((f) => ({ ...f, direccion: e.target.value }))}
            />
          </label>
          <label className="span-2">
            Correo
            <input
              value={form.correoElectronico}
              onChange={(e) => setForm((f) => ({ ...f, correoElectronico: e.target.value }))}
            />
          </label>
        </div>
        <div className="actions">
          <button type="button" onClick={handleSave} disabled={loading}>
            {editingId != null ? 'Actualizar' : 'Crear'}
          </button>
          <button type="button" className="secondary" onClick={resetForm} disabled={loading}>
            Nuevo
          </button>
          <button type="button" className="danger" onClick={() => void handleDelete()} disabled={loading || editingId == null}>
            Eliminar
          </button>
        </div>
      </section>

      <section className="panel">
        <h2>Búsqueda por nombre</h2>
        <div className="search-row">
          <input
            placeholder="Texto en el nombre…"
            value={searchNombre}
            onChange={(e) => setSearchNombre(e.target.value)}
            onKeyDown={(e) => e.key === 'Enter' && void handleSearch()}
          />
          <button type="button" onClick={() => void handleSearch()} disabled={loading}>
            Buscar
          </button>
          <button type="button" className="secondary" onClick={() => void loadAll()} disabled={loading}>
            Ver todos
          </button>
        </div>
      </section>

      <section className="panel">
        <h2>Listado {loading && <span className="muted">(cargando…)</span>}</h2>
        <div className="table-wrap">
          <table>
            <thead>
              <tr>
                <th>Id</th>
                <th>Nombre</th>
                <th>Edad</th>
                <th>Dirección</th>
                <th>Correo</th>
              </tr>
            </thead>
            <tbody>
              {clientes.length === 0 ? (
                <tr>
                  <td colSpan={5} className="muted">
                    Sin resultados
                  </td>
                </tr>
              ) : (
                clientes.map((c) => (
                  <tr
                    key={c.id}
                    className={editingId === c.id ? 'selected' : undefined}
                    onClick={() => selectRow(c)}
                  >
                    <td>{c.id}</td>
                    <td>{c.nombre}</td>
                    <td>{c.edad}</td>
                    <td>{c.direccion}</td>
                    <td>{c.correoElectronico}</td>
                  </tr>
                ))
              )}
            </tbody>
          </table>
        </div>
      </section>
    </div>
  )
}

export default App
