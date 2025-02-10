import React, { useState } from "react";
import { Button } from "../ui/button";
import { Dialog, DialogClose, DialogContent, DialogDescription, DialogFooter, DialogHeader, DialogTitle, DialogTrigger } from "../ui/dialog";
import { Input } from "../ui/input";
import { toast } from "@/hooks/use-toast";
import { Pencil } from "lucide-react";

interface EditButtonInput{
    domain: string,
    password: string
}

const updateRecord = async (domain:string, password:string) => {
    const requestBody = {
        "domain": domain,
        "password": password
    }
    await fetch(process.env.NEXT_PUBLIC_RECORD_API_URL!,{
        method: 'PUT',
        body: JSON.stringify(requestBody)
    })
}

const EditButton: React.FC<EditButtonInput> = ({domain, password}) =>
{
    const [getPassword, setPassword] = useState(password)

    return (
        <Dialog>
            <DialogTrigger asChild>
                <Button><Pencil/>Edit</Button>
            </DialogTrigger>
            <DialogContent>
                <DialogHeader>
                    <DialogTitle>Edit record</DialogTitle>
                    <DialogDescription>Please fill in new password for {domain}</DialogDescription>
                </DialogHeader>
                <Input placeholder="New password" type="password" value={getPassword} onChange={(event) => setPassword(event.target.value)}></Input>
                <DialogFooter>
                    <DialogClose asChild>
                        <Button onClick={() => {
                            toast({
                                title: "Updating record for you",
                                description: "We will reload the page once the update is finish"
                            })
                            updateRecord(domain,getPassword)
                            .then(() => {
                                window.location.reload()
                            })
                        }}>Save</Button>
                    </DialogClose>
                </DialogFooter>
            </DialogContent>
        </Dialog>
    )
};

export default EditButton;