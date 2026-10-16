public class Fish {
    private String name;
    private double weight;
    private double price;

    public Fish(String name, double weight, double price){
        this.name = name;
        this.weight = weight;
        this.price = price;
    }

    @Override
    public String toString(){
        return this.name+ " weight = " + this.weight + " price = " + this.price;
    }

    @Override
    public boolean equals(Object o){
        if(o==this) return true;
        if(!(o instanceof Fish tmp)){
            return false;
        }
        return(tmp.name.equals(this.name) &&
                tmp.weight == this.weight && tmp.price==this.price);
    }

    @Override
}
