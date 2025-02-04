import { useState } from "react";
import { Button } from "../ui/button";
import { Dialog, DialogClose, DialogContent, DialogDescription, DialogFooter, DialogHeader, DialogTitle, DialogTrigger } from "../ui/dialog";
import { Input } from "../ui/input";
import { useRouter } from "next/navigation";
import { toast } from "@/hooks/use-toast";
import { PlusCircle } from "lucide-react";

const CreateNewRecord = async (domain:string, password:string) => {
    const requestBody = {
        "domain": domain,
        "password": password
    }
    await fetch(process.env.NEXT_PUBLIC_RECORD_API_URL!,{
        method: 'POST',
        body: JSON.stringify(requestBody),
        headers: {'content-type':'application/json'}
    })
}

const CreateNewRecordButton = () =>
{
    const [getDomain, setDomain] = useState('');
    const [getPassword, setPassword] = useState('');
    return(
        <div className="m-2">
            <Dialog>
                <DialogTrigger asChild>
                    <Button className="w-full" size="lg">
                        <PlusCircle/>
                        Create new record
                    </Button>
                </DialogTrigger>
                <DialogContent>
                    <DialogHeader>
                        <DialogTitle>Create new record</DialogTitle>
                        <DialogDescription>Please fill in your Namecheap domain and password for updating DDNS</DialogDescription>
                    </DialogHeader>
                        <Input placeholder="Domain name" onChange={(event) => setDomain(event.target.value)}></Input>
                        <Input placeholder="Password" type="password" onChange={(event) => setPassword(event.target.value)}></Input>
                    <DialogFooter>
                        <DialogClose>
                            <Button onClick={() => {
                                toast({
                                    title: "Creating new record for you",
                                    description: "We will refresh the page once the record is created"
                                })
                                CreateNewRecord(getDomain,getPassword)
                                .then(() => {
                                    window.location.reload()
                                })
                                }
                            }>Save</Button>
                        </DialogClose>
                    </DialogFooter>
                </DialogContent>
            </Dialog>
        </div>
)};

export default CreateNewRecordButton;