import { toast } from "@/hooks/use-toast";
import { Button } from "../ui/button";
import React from "react";
import { Trash2 } from "lucide-react";

interface DeleteButtonInput{
    domain:string
}

const deleteRecord = async (domain:string) => {
    await fetch(process.env.NEXT_PUBLIC_RECORD_API_URL! + "/" + domain,{
        method: 'DELETE'
    })
}

const DeleteButton: React.FC<DeleteButtonInput> = ({domain}) =>
{
    return (
        <Button variant="destructive" onClick={() => {
            deleteRecord(domain)
            .then(() => {
                toast({
                    title: "Deleting your domain record",
                    description: "I will reload the page once it is done"
                })
            })
            .then(() => {
                window.location.reload()
            })
        }}
        >
            <Trash2/>Delete</Button>
    )
};

export default DeleteButton;