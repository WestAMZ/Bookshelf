import { Outlet } from 'react-router'
import { Navbar } from './Navbar'

export const MainLayout = () => {
  return (
    <>
        <Navbar/>
        <Outlet/>
    </>
  )
}
