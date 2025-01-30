import Link from "next/link"
import { Home, Settings, Info } from "lucide-react"

export default function Header() {
  return (
    <header className="shadow-md">
      <div className="container px-4 py-4 flex justify-between">
        <h1 className="text-2xl font-bold">Namecheap DDNS Updater</h1>
      </div>
    </header>
  )
}