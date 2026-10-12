import java.util.Scanner;

class Program {
    public static void main(String[] args){
//        first();
//        second();
//        third();
    }

    public static void first(){
        String line = "Fall seven times and stand up eight";
        String[] words = line.split(" ");

        for (String word : words) {
            System.out.println(word);
        }
    }

    public static void second() {
        Scanner n1 = new Scanner(System.in);
        System.out.print("Enter 1 num = ");
        int num1 = 0;
        if (n1.hasNextInt()) {
            num1 = n1.nextInt();
        } else {
            System.out.println("Its not a number");
        }

        Scanner n2 = new Scanner(System.in);
        System.out.print("Enter 2 num = ");
        int num2 = 0;
        if (n2.hasNextInt()) {
            num2 = n2.nextInt();
        } else {
            System.out.println("Its not a number");
        }
        if(num1 == (int)num1 && num2==(int)num2 ){
            System.out.println("Plus = " +(num1 + num2));
            System.out.println("Minus = " +(num1 - num2));
            System.out.println("Multipl = " +(num1 * num2));
        }
        else{
            System.out.println("Something was wrong");
        }
    }

    public static void third(){
        Scanner sc = new Scanner(System.in);
        System.out.print("Number for reverse: ");
        int n = 0;

        if(sc.hasNextInt()){
            n = sc.nextInt();
        }
        else{System.out.print("Its not a number");}

        String str = Integer.toString(n);
        String reversed = new StringBuilder(str).reverse().toString();
        System.out.print(reversed);
    }
}
