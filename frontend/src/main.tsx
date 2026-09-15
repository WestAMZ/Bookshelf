import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import { BookshelfApp } from './BookshelfApp'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <BookshelfApp/>
  </StrictMode>,
)
