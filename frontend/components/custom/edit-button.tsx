import { Button } from "../ui/button";
import { Dialog, DialogClose, DialogContent, DialogDescription, DialogFooter, DialogHeader, DialogTitle, DialogTrigger } from "../ui/dialog";
import { Input } from "../ui/input";

export default function EditButton()
{
    return (
        <Dialog>
            <DialogTrigger asChild>
                <Button>Edit</Button>
            </DialogTrigger>
            <DialogContent>
                <DialogHeader>
                    <DialogTitle>Edit record</DialogTitle>
                    <DialogDescription>Please fill in new domain record and password</DialogDescription>
                </DialogHeader>
                <Input placeholder="Domain name"></Input>
                <Input placeholder="Password" type="password"></Input>
                <DialogFooter>
                    <DialogClose asChild>
                        <Button>Save</Button>
                    </DialogClose>
                </DialogFooter>
            </DialogContent>
        </Dialog>
    )
}