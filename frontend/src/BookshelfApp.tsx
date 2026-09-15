import { RouterProvider } from 'react-router'
import { AppRouter } from './router'
import { AuthProvider } from './contexts/AuthContext'

export const BookshelfApp = () => {
  return (
    <AuthProvider>
      <RouterProvider router={AppRouter} />
    </AuthProvider>    
  )
}
