import { useState } from "react";
import { Button } from "../ui/button";
import { Dialog, DialogClose, DialogContent, DialogDescription, DialogFooter, DialogHeader, DialogTitle, DialogTrigger } from "../ui/dialog";
import { Input } from "../ui/input";
import { useRouter } from "next/navigation";

const CreateNewRecord = async (domainInput:string, passwordInput:string) => {
    const requestBody = {
        "domain": domainInput,
        "password": passwordInput
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
    const router = useRouter();
    return(
        <div className="m-2">
            <Dialog>
                <DialogTrigger asChild>
                    <Button className="w-full" size="lg">
                        <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor" className="size-6">
                            <path strokeLinecap="round" strokeLinejoin="round" d="M12 9v6m3-3H9m12 0a9 9 0 1 1-18 0 9 9 0 0 1 18 0Z" />
                        </svg>
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
                                CreateNewRecord(getDomain,getPassword)
                                .then(router.refresh)
                                }}>Save</Button>
                        </DialogClose>
                    </DialogFooter>
                </DialogContent>
            </Dialog>
        </div>
)};

export default CreateNewRecordButton;